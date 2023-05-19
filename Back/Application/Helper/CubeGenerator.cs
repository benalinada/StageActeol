using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Microsoft.AnalysisServices;
    using System.Data.SqlClient;
    using System.Data;
    using System.Data.OleDb;
    using System.CodeDom.Compiler;
    using Application.Common.Interfaces;
    using Application.Cube.Commandes.CreateCubeCommand;
    using Domain.Common.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Xml.Linq;
    using System.Data.Common;
   
    namespace OLAPCube
    {
        public static class CubeGenerator
        {



            #region Cube Generation.

            public static void BuildCube(CreateCubeCommand command, bool isEmptyCube = false)
            {
                try
                {


                    Server objServer = new Server();
                    Database objDatabase = new Database();
                    RelationalDataSource objDataSource = new RelationalDataSource();
                    DataSourceView objDataSourceView = new DataSourceView();
                    DataSet objDataSet = new DataSet();
                    Dimension[] objDimensions = new Dimension[command.DimensionTableCount];

                    //Connecting to the Analysis Services.
                    objServer = (Server)ConnectAnalysisServices(command.DBAnalyserServerName, command.ProviderName);
                    //Creating a Database.
                    objDatabase = (Database)CreateDatabase(objServer, command.CubeDBName);
                    //Creating a DataSource.
                    objDataSource = (RelationalDataSource)CreateDataSource(objServer, objDatabase, command.CubeDataSourceName, command.DBEngineServer, command.DBName);
                    if (!isEmptyCube)
                    {
                        //Creating a DataSourceView.
                        objDataSet = (DataSet)GenerateDWSchema(command.DBEngineServer, command.DBName, command.FactTableName, command.TableNamesAndKeys, command.DimensionTableCount);
                        objDataSourceView = (DataSourceView)CreateDataSourceView(objDatabase, objDataSource, objDataSet, command.CubeDataSourceViewName);
                        //Creating the Dimension, Attribute, Hierarchy, and MemberProperty Objects.                
                        objDimensions = (Dimension[])CreateDimension(objDatabase, objDataSourceView, command.TableNamesAndKeys, command.DimensionTableCount);
                        //Creating the Cube, MeasureGroup, Measure, and Partition Objects.

                        CreateCube(objDatabase, objDataSourceView, objDataSource, objDimensions, command.FactTableName, command.TableNamesAndKeys, command.DimensionTableCount , command.Messurecalcl , command.MessureCout);

                    }
                    objDatabase.Process(ProcessType.ProcessFull);


                    Console.WriteLine("Cube created successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error -> " + ex.Message);
                    throw new Exception("error cret cube ");
                }

                Console.WriteLine("Press any key to exit.");

            }

            #region Connecting to the Analysis Services.
            /// <summary>
            /// Connecting to the Analysis Services.
            /// </summary>
            /// <param name="strDBServerName">Database Server Name.</param>
            /// <param name="strProviderName">Provider Name.</param>
            /// <returns>Database Server instance.</returns>
            public static object ConnectAnalysisServices(string serverName, string strProviderName)
            {
                try
                {
                    Console.WriteLine("Connecting to the Analysis Services ...");

                    Server objServer = new Server();
                    //string strConnection = "Data Source=" + strDBServerName + ";";
                    string strConnection = $"Data Source={serverName}; Provider={strProviderName};Persist Security Info=True;Password=0000;User ID=sa";
                    //Disconnect from current connection if it's currently connected.
                    if (objServer.Connected)
                        objServer.Disconnect();
                    else
                        objServer.Connect(strConnection);

                    return objServer;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in Connecting to the Analysis Services. Error Message -> " + ex.Message);
                    return null;
                }
            }
            #endregion Connecting to the Analysis Services.

            #region Creating a Database.
            /// <summary>
            /// Creating a Database.
            /// </summary>
            /// <param name="objServer">Database Server Name.</param>
            /// <param name="strCubeDBName">Cube DB Name.</param>
            /// <returns>DB instance.</returns>
            private static object CreateDatabase(Server objServer, string strCubeDBName)
            {
                try
                {
                    Console.WriteLine("Creating a Database ...");

                    Database objDatabase = new Database();
                    //Add Database to the Analysis Services.
                    objDatabase = objServer.Databases.Add(objServer.Databases.GetNewName(strCubeDBName));
                    //Save Database to the Analysis Services.
                    // objDatabase.StorageEngineUsed = StorageEngineUsed.InMemory;
                    objDatabase.Update();

                    return objDatabase;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in Creating a Database. Error Message -> " + ex.Message);
                    return null;
                }
            }
            #endregion Creating a Database.

            #region Creating a DataSource.
            /// <summary>
            /// Creating a DataSource.
            /// </summary>
            /// <param name="objServer">Database Server Name.</param>
            /// <param name="objDatabase">Database Name.</param>
            /// <param name="strCubeDataSourceName">Cube DataSource Name.</param>
            /// <param name="strDBServerName">DB Server Name.</param>
            /// <param name="strDBName">DB Name.</param>
            /// <returns>DataSource instance.</returns>
            private static object CreateDataSource(Server objServer, Database objDatabase, string strCubeDataSourceName, string strDBServerName, string strDBName)
            {
                try
                {

                    RelationalDataSource objDataSource = new RelationalDataSource();
                    //Add Data Source to the Database.
                    objDataSource = objDatabase.DataSources.Add(objServer.Databases.GetNewName(strCubeDataSourceName));
                    // objDataSource.ConnectionString = "Provider=SQLOLEDB.1;Initial Catalog=DW_Bookings ;Data Source=DESKTOP-0159C82\\VE_SERVER;Integrated Security=true;TrustServerCertificate=True;";
                    objDataSource.ConnectionString = "Provider=sqloledb; Data Source=DESKTOP-0159C82\\VE_SERVER ; Initial Catalog=" + strDBName + "; Persist Security Info=True;Password=1234;User ID=sa";
                    objDataSource.Update();

                    return objDataSource;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in Creating a DataSource. Error Message -> " + ex.Message);
                    return null;
                }
            }
            #endregion Creating a DataSource.

            #region Creating a DataSourceView.
            /// <summary>
            /// Creating a DataSourceView.
            /// </summary>
            /// <param name="strDBServerName">DB Server Name.</param>
            /// <param name="strDBName">DB Name.</param>
            /// <param name="strFactTableName">FactTable Name.</param>
            /// <param name="strTableNamesAndKeys">Array of TableNames and Keys.</param>
            /// <param name="intDimensionTableCount">Dimension Table Count.</param>
            /// <returns>DataSet instance.</returns>
            private static object GenerateDWSchema(string strDBServerName, string strDBName, string strFactTableName, string[,] strTableNamesAndKeys, int intDimensionTableCount)
            {
                try
                {

                    //Create the connection string.
                    string conxString = "Data Source=DESKTOP-0159C82\\VE_SERVER; Initial Catalog=" + strDBName + "; Integrated Security=True;";
                    //Create the SqlConnection.
                    SqlConnection objConnection = new SqlConnection(conxString);
                    DataSet objDataSet = new DataSet();
                    //Add FactTable in DataSet.
                    objDataSet = (DataSet)FillDataSet(objConnection, objDataSet, strFactTableName);
                    //Add table in DataSet and Relation between them.
                    for (int i = 0; i < intDimensionTableCount; i++)
                    {
                        //Retrieve table's schema and assign the table's schema to the DataSet.
                        //Add primary key to the schema according to the primary key in the tables.
                        objDataSet = (DataSet)FillDataSet(objConnection, objDataSet, strTableNamesAndKeys[i, 0]);
                        objDataSet = (DataSet)AddDataTableRelation(objDataSet, strTableNamesAndKeys[i, 0], strTableNamesAndKeys[i, 1], strTableNamesAndKeys[i, 2], strTableNamesAndKeys[i, 3]);
                    }

                    return objDataSet;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in Creating a DataSourceView - GenerateDWSchema. Error Message -> " + ex.Message);
                    return null;
                }
            }
            /// <summary>
            /// Fill the DataSet with DataTables.
            /// </summary>
            /// <param name="objConnection">Connection instance.</param>
            /// <param name="objDataSet">DataSet instance.</param>
            /// <param name="strTableName">TableName.</param>
            /// <returns>DataSet instance.</returns>
            private static object FillDataSet(SqlConnection objConnection, DataSet objDataSet, string strTableName)
            {
                try
                {
                    string strCommand = "Select * from " + strTableName;
                    SqlDataAdapter objEmpData = new SqlDataAdapter(strCommand, objConnection);
                    objEmpData.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    objEmpData.FillSchema(objDataSet, SchemaType.Source, strTableName);

                    return objDataSet;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in Creating a DataSourceView - FillDataSet. Error Message -> " + ex.Message);
                    return null;
                }
            }
            /// <summary>
            /// Add relations between DataTables of DataSet.
            /// </summary>
            /// <param name="objDataSet">DataSet instance.</param>
            /// <param name="strParentTableName">Parent Table Name (Dimension Table).</param>
            /// <param name="strParentTableKey">Parent Table Key.</param>
            /// <param name="strChildTableName">Child Table Name (Fact Table).</param>
            /// <param name="strChildTableKey">Child Table Key.</param>
            /// <returns>DataSet instance.</returns>
            private static object AddDataTableRelation(DataSet objDataSet, string strParentTableName, string strParentTableKey, string strChildTableName, string strChildTableKey)
            {
                try
                {
                    objDataSet.Relations.Add(strChildTableName + "_" + strParentTableName + "_FK", objDataSet.Tables[strParentTableName].Columns[strParentTableKey], objDataSet.Tables[strChildTableName].Columns[strChildTableKey]);

                    return objDataSet;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in Creating a DataSourceView - AddDataTableRelation. Error Message -> " + ex.Message);
                    return null;
                }
            }


            /// <summary>
            /// Creating a DataSourceView.
            /// </summary>
            /// <param name="objDatabase">DB instance.</param>
            /// <param name="objDataSource">DataSource instance.</param>
            /// <param name="objDataSet">DataSet instance.</param>
            /// <param name="strCubeDataSourceViewName">Cube DataSourceView Name.</param>
            /// <returns>DataSourceView instance.</returns>
            private static object CreateDataSourceView(Database objDatabase, RelationalDataSource objDataSource, DataSet objDataSet, string strCubeDataSourceViewName)
            {
                try
                {
                    DataSourceView objDataSourceView = new DataSourceView();
                    //Add Data Source View to the Database.
                    objDataSourceView = objDatabase.DataSourceViews.Add(objDatabase.DataSourceViews.GetNewName(strCubeDataSourceViewName));
                    objDataSourceView.DataSourceID = objDataSource.ID;
                    objDataSourceView.Schema = objDataSet;
                    objDataSourceView.Update();

                    return objDataSourceView;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in Creating a DataSourceView - CreateDataSourceView. Error Message -> " + ex.Message);
                    return null;
                }
            }
            #endregion Creating a DataSourceView.

            #region Creating a Creating the Dimension, Attribute, Hierarchy, and MemberProperty Objects.
            /// <summary>
            /// Creating the Dimension, Attribute, Hierarchy, and MemberProperty Objects.
            /// </summary>
            /// <param name="objDatabase">DB instance.</param>
            /// <param name="objDataSourceView">DataSource instance.</param>
            /// <param name="strTableNamesAndKeys">Array of Table names and keys.</param>
            /// <param name="intDimensionTableCount">Dimension table count.</param>
            /// <returns>Dimension Array.</returns>
            private static object[] CreateDimension(Database objDatabase, DataSourceView objDataSourceView, string[,] strTableNamesAndKeys, int intDimensionTableCount)
            {
                try
                {
                    Console.WriteLine("Creating the Dimension, Attribute, Hierarchy, and MemberProperty Objects ...");

                    Dimension[] objDimensions = new Dimension[intDimensionTableCount];
                    for (int i = 0; i < intDimensionTableCount; i++)
                    {
                        objDimensions[i] = (Dimension)GenerateDimension(objDatabase, objDataSourceView, strTableNamesAndKeys[i, 0], strTableNamesAndKeys[i, 1]);
                    }

                    ////Add Hierarchy and Level
                    //Hierarchy objHierarchy = objDimension.Hierarchies.Add("ProductByCategory");
                    //objHierarchy.Levels.Add("Category").SourceAttributeID = objCatKeyAttribute.ID;
                    //objHierarchy.Levels.Add("Product").SourceAttributeID = objProdKeyAttribute.ID;
                    ////Add Member Property
                    ////objProdKeyAttribute.AttributeRelationships.Add(objProdDescAttribute.ID);
                    //objDimension.Update();

                    return objDimensions;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in Creating the Dimension, Attribute, Hierarchy, and MemberProperty Objects. Error Message -> " + ex.Message);
                    return null;
                }
            }
            /// <summary>
            /// Generate single dimension.
            /// </summary>
            /// <param name="objDatabase">DB instance.</param>
            /// <param name="objDataSourceView">DataSourceView instance.</param>
            /// <param name="strTableName">Table name.</param>
            /// <param name="strTableKeyName">Table key.</param>
            /// <returns>Dimension instance.</returns>
            private static object GenerateDimension(Database objDatabase, DataSourceView objDataSourceView, string strTableName, string strTableKeyName)
            {
                try
                {
                    Dimension objDimension = new Dimension();

                    //Add Dimension to the Database
                    objDimension = objDatabase.Dimensions.Add(strTableName);
                    objDimension.Source = new DataSourceViewBinding(objDataSourceView.ID);
                    DimensionAttributeCollection objDimensionAttributesColl = objDimension.Attributes;
                    //Add Dimension Attributes
                    DimensionAttribute objAttribute = objDimensionAttributesColl.Add(strTableKeyName);
                    //Set Attribute usage and source
                    objAttribute.Usage = AttributeUsage.Key;
                    objAttribute.KeyColumns.Add(strTableName, strTableKeyName, DataType.Int32);

                    objDimension.Update();

                    return objDimension;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in Creating the Dimension, Attribute, Hierarchy, and MemberProperty Objects - GenerateDimension. Error Message -> " + ex.Message);
                    return null;
                }
            }
            #endregion Creating a Creating the Dimension, Attribute, Hierarchy, and MemberProperty Objects.

            #region Creating the Cube, MeasureGroup, Measure, and Partition Objects.
            /// <summary>
            /// Creating the Cube, MeasureGroup, Measure, and Partition Objects.
            /// </summary>
            /// <param name="objDatabase">DB instance.</param>
            /// <param name="objDataSourceView">DataSourceView instance.</param>
            /// <param name="objDataSource">DataSource instance.</param>
            /// <param name="objDimensions">Dimensions array instance.</param>
            /// <param name="strFactTableName">FactTable Name.</param>
            /// <param name="strTableNamesAndKeys">Array of Table Names and Keys.</param>
            /// <param name="intDimensionTableCount">DimensionTable Count.</param>
            private static void CreateCube(Database objDatabase, DataSourceView objDataSourceView, RelationalDataSource objDataSource, Dimension[] objDimensions, string strFactTableName, string[,] strTableNamesAndKeys, int intDimensionTableCount , string[,] Messurecalcl , int MessureCout )
            {
                try
                {
                    Console.WriteLine("Creating the Cube, MeasureGroup, Measure, and Partition Objects ...");
                    Cube objCube = new Cube();
                    
                    MdxScript objTotal = new MdxScript();
                    String strScript;

                    Partition objPartition = new Partition();
                    Command objCommand = new Command();
                    //Add Cube to the Database and set Cube source to the Data Source View
                    objCube = objDatabase.Cubes.Add("SampleCube");
                    objCube.Source = new DataSourceViewBinding(objDataSourceView.ID);
                    //Add Measure Group to the Cube
                    //MeasureGroup objMeasureGroup = objCube.MeasureGroups.Add("FactSales");
                    MeasureGroup objMeasureGroup = objCube.MeasureGroups.Add(strFactTableName);
                    // plusieur 
                  
                    for (int i = 0; i < MessureCout ; i++)
                    {
                        Measure objMessure = new Measure();

                        //Add Measure to the Measure Group and set Measure source
                        objMessure = objMeasureGroup.Measures.Add(Messurecalcl[i,0]);
                        
                        objMessure.Source = new DataItem(strFactTableName, Messurecalcl[i,0], DataType.Int32);
                        objMessure.Name = Messurecalcl[i, 2];
                        if(Messurecalcl[i, 1]== "Sum")
                        {
                            objMessure.AggregateFunction = AggregationFunction.Sum;

                        }else if (Messurecalcl[i, 1] == "Distinctcount")
                        {
                            objMessure.AggregateFunction = AggregationFunction.DistinctCount;
                        }
                        else if (Messurecalcl[i, 1] == "count")
                        {
                            objMessure.AggregateFunction = AggregationFunction.Count;
                        }
                        else if (Messurecalcl[i, 1] == "Avg")
                        {
                            objMessure.AggregateFunction = AggregationFunction.AverageOfChildren;
                        }
                        else if (Messurecalcl[i, 1] == "Min")
                        {
                            objMessure.AggregateFunction = AggregationFunction.Min;
                        }
                        else if (Messurecalcl[i, 1] == "Max")
                        {
                            objMessure.AggregateFunction = AggregationFunction.Max;
                        }

                    }
                    

                    // objQuantity = objMeasureGroup.Measures.Add("Quantity");
                    //objQuantity.Source = new DataItem(strFactTableName, "OrderQuantity", DataType.Int32);

                    ////Calculated Member Definition
                    //strScript = "Calculated; Create Member CurrentCube.[Measures].[Total] As [Measures].[Quantity] * [Measures].[Amount]";

                    ////Add Calculated Member
                    //objTotal.Name = "Total Sales";
                    //objCommand.Text = strScript;
                    //objTotal.Commands.Add(objCommand);

                    //objCube.MdxScripts.Add(objTotal);

                    for (int i = 0; i < intDimensionTableCount; i++)
                    {
                        GenerateCube(objCube, objDimensions[i], objMeasureGroup, strFactTableName, strTableNamesAndKeys[i, 3]);
                    }

                    objPartition = objMeasureGroup.Partitions.Add(strFactTableName);
                    objPartition.Source = new TableBinding(objDataSource.ID, "dbo", strFactTableName);

                    objPartition.ProcessingMode = ProcessingMode.Regular;
                    objPartition.StorageMode = StorageMode.Molap;
                    //Save Cube and all major objects to the Analysis Services
                    objCube.Update(UpdateOptions.ExpandFull);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in Creating the Cube, MeasureGroup, Measure, and Partition Objects. Error Message -> " + ex.Message);
                }
            }
            /// <summary>
            /// Generate cube.
            /// </summary>
            /// <param name="objCube">Cube instance.</param>
            /// <param name="objDimension">Dimension instance.</param>
            /// <param name="objMeasureGroup">MeasureGroup instance.</param>
            /// <param name="strFactTableName">FactTable Name.</param>
            /// <param name="strTableKey">Table Key.</param>
            private static void GenerateCube(Cube objCube, Dimension objDimension, MeasureGroup objMeasureGroup, string strFactTableName, string strTableKey)
            {
                try
                {
                    CubeDimension objCubeDim = new CubeDimension();
                    RegularMeasureGroupDimension objRegMGDim = new RegularMeasureGroupDimension();
                    MeasureGroupAttribute objMGA = new MeasureGroupAttribute();
                    //Add Dimension to the Cube
                    objCubeDim = objCube.Dimensions.Add(objDimension.ID);
                    //Use Regular Relationship Between Dimension and FactTable Measure Group
                    objRegMGDim = objMeasureGroup.Dimensions.Add(objCubeDim.ID);
                    //Link TableKey in DimensionTable with TableKey in FactTable Measure Group
                    objMGA = objRegMGDim.Attributes.Add(objDimension.KeyAttribute.ID);

                    objMGA.Type = MeasureGroupAttributeType.Granularity;
                    objMGA.KeyColumns.Add(strFactTableName, strTableKey, DataType.Int32);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in Creating the Cube, MeasureGroup, Measure, and Partition Objects - GenerateCube. Error Message -> " + ex.Message);
                }
            }
            #endregion Creating the Cube, MeasureGroup, Measure, and Partition Objects.

            #endregion Cube Generation.

            public static IEnumerable<DataBase> GetDataBaseAnalyser(string serverName, string provider)
            {
                var server = (Microsoft.AnalysisServices.Server)CubeGenerator.ConnectAnalysisServices(serverName, provider);

                var listdb = new List<DataBase>();

                foreach (Database db in server.Databases)
                {
                    listdb.Add(new DataBase { Name = db.Name, Id = new Guid() });
                }

                return listdb;
            }
            public static Cube GetCube(string serverName, string dbName, string cubeName, string provider)
            {
                var server = (Microsoft.AnalysisServices.Server)CubeGenerator.ConnectAnalysisServices(serverName, provider);

                var db = server.Databases.FindByName(dbName);
                return db.Cubes.FindByName(cubeName);

            }
            public static void Clone(string serverSourceName, string dbSourceName, string serverTargetName, string dbTargetName, string cubeName, string provider)
            {
                var existCube = GetCube(serverSourceName, dbSourceName, cubeName, provider);
                var newEmptyCube = GetCube(serverTargetName, dbTargetName, cubeName, provider);
                if (existCube == null || newEmptyCube == null)
                {
                    throw new Exception("Cube not found");
                }
                var serverTarget = (Microsoft.AnalysisServices.Server)CubeGenerator.ConnectAnalysisServices(serverTargetName, provider);
                var db = serverTarget.Databases.FindByName(dbTargetName);
                db.Cubes.Add(existCube);
                db.Update();
            }

            public static IEnumerable<MessureCube> GetMessure(string serverName, string dbName, string provider)
            {
                var server = (Microsoft.AnalysisServices.Server)CubeGenerator.ConnectAnalysisServices(serverName, provider);

                var db = server.Databases.FindByName(dbName);
                var mesures = db.Cubes.FindByName("SampleCube").AllMeasures;
                var data = mesures.Cast<Microsoft.AnalysisServices.Measure>().ToList();

                return data.Select(m => new MessureCube() { Name = m.Name });
            }
            public static IEnumerable<CalculationCube> GetCalculation(string serverName, string dbName, string provider)
            {
                var server = (Microsoft.AnalysisServices.Server)CubeGenerator.ConnectAnalysisServices(serverName, provider);

                var db = server.Databases.FindByName(dbName);
                var calcul = db.Cubes.FindByName("SampleCube").DefaultMdxScript;
                var data = calcul.Annotations;
                return null;
               // return data.Select(m => new Calculation() { Name = m.Name });
            }


            public static void  Calculation(  string serverName, string dbName, string provider, string Mes1, string Mes2, string ope, string NameCalculation)
            {
                Command objCommand = new Command();
                MdxScript objTotal = new MdxScript();
                String strScript;
                var server = (Microsoft.AnalysisServices.Server)CubeGenerator.ConnectAnalysisServices(serverName, provider);

                var db = server.Databases.FindByName(dbName).Cubes.FindByName("SampleCube");

                //Calculated Member Definition
               
                strScript = $"Calculated; Create Member CurrentCube.[Measures].[Total] As [Measures].[{Mes1}] {ope} [Measures].[{Mes2}]";

                //Add Calculated Member
                objTotal.Name = NameCalculation;
                objCommand.Text = strScript;
                objTotal.Commands.Add(objCommand);
                db.MdxScripts.Add(objTotal);
                

            }

          
        }
    }
}

