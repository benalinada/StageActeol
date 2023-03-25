import { ViewEncapsulation } from '@angular/compiler';
import { Component, OnInit, Inject } from '@angular/core';
import { expand, flyInOut } from 'src/app/animations/app.animation';
import { DishService } from 'src/app/services/services/dish.service';
import { LeaderService } from 'src/app/services/services/leader.service';
import { PromotionService } from 'src/app/services/services/promotion.service';
import { Leader } from 'src/app/services/shared/leader';
import { Promotion } from 'src/app/services/shared/promotion';
import { Dish } from './dish';




@Component({
  selector: 'app-icons',
  templateUrl: './icons.component.html',
  styleUrls: ['./icons.component.scss'],
  
host: {
  '[@flyInOut]': 'true',
  'style': 'display: block;'
  },
  animations: [
    flyInOut(),
    expand()

  ]
})
export class IconsComponent implements OnInit{

  dish: Dish;
  dishErrMess: string;
  errMess: string;
  lerrMess: string;



  promotion: Promotion;
  leader: Leader;

  constructor(private dishservice: DishService,
    private leaderservice: LeaderService,
    private promotionservice: PromotionService,
    @Inject('BaseURL') public BaseURL) { }

    ngOnInit() {
      this.dishservice.getFeaturedDish()
      .subscribe(dish => this.dish,
        errmess => this.dishErrMess = <any>errmess);
      this.promotionservice.getFeaturedPromotion()
      .subscribe(promotion => this.promotion = promotion,
        errmess => this.errMess = <any>errmess);
      this.leaderservice.getFeaturedLeader()
      .subscribe(leader => this.leader = leader,
        lerrmess => this.lerrMess = <any>lerrmess);
    }


}

