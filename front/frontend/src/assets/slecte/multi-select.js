$(function () {
    var multiSelect = $('[data-multiple]');
    
    multiSelect.each(function () {
        var $this = $(this),
            numberOfOptions = $this.children('option'),
            listOption = $(),
            previewOption = $(),
            previewCloseButton = $();
            
        
        if (!$this.find('option:selected').length)
            $this.addClass('empty');
        
        // Initially append class to select so it can be hidden
        $this.addClass('select-hidden');
        // Create wrappers and initial placeholders
        $this.wrap('<div class="multi-select"></div>');
        $this.after('<div class="multi-select-preview"></div>');     
        var selectedOptions = $this.next('div.multi-select-preview');
        var selectOptionsListWrapper = $('<div />', {
                'class': 'multi-select-options-wrapper'
            }).insertAfter(selectedOptions);
        
        var selectOptionsList = $('<ul />', {
                'class': 'multi-select-options-list'
            }).appendTo(selectOptionsListWrapper);
        
        // Loop through all options and create list items that can be easily styled
        numberOfOptions.each(function (index, item) {
            var $item = $(item);
            listOption = listOption.add($('<li />', {
                text: $item.text(),
                rel: $item.val(),
                'class': $item.attr('selected') ? 'selected' : ''
            })).appendTo(selectOptionsList);
            
            previewOption = previewOption.add($('<div />', {
                'class': $item.attr('selected') ? 'selected-value active' : 'selected-value'
            })).appendTo(selectedOptions);
            
            $('<span />', {
                text: '×',
                'class': 'selected-value-icon',
                'aria-hidden': true
            }).appendTo(previewOption.eq(index));
            
            $('<span />', {
                text: $item.text(),
                'class': 'selected-value-label',
                'aria-selected': $item.attr('selected') ? true : false
            }).appendTo(previewOption.eq(index));
        });
        
        var buttonClearAll = $('<span />', {
            'class': true && $this.find('option:selected').length ? 'multi-select-clear' : 'multi-select-clear hidden',
            text: '×'
        }).appendTo(selectedOptions);
        
        var buttonArrow = $('<span />', {
            'class': 'multi-select-arrow'
        }).appendTo(selectedOptions);
        
        // Functionality on preview click
        selectedOptions.on('click', function (e) {
            e.stopPropagation();
            var $self = $(this);
            
            $('div.multi-select-preview.active').not(this).each(function () {
                $(this).removeClass('active').next('.multi-select-options-wrapper').hide();
            });
            
            $self.toggleClass('active').next('.multi-select-options-wrapper').toggle();
        });
        // Put close icon buttons in variable after initialization
        previewCloseButton = selectedOptions.find('.selected-value-icon');
        
        // Hide everything opened on document click
        $(document).on('click', function () {
            selectedOptions.removeClass('active');
            selectOptionsListWrapper.hide();
        });
        
        // On escape key close selecte
        $(document).keyup(function(e) {
             if (selectOptionsListWrapper.is(':visible') && e.keyCode == 27) { 
                selectedOptions.removeClass('active');
                selectOptionsListWrapper.hide();
            }
        });
        
        // Functionality on picked option
        listOption.on('click', function (e) {
            e.stopPropagation();
            var $self = $(this);
            
            var index = $self.index();
            if ($self.hasClass('selected')) {
                $self.removeClass('selected');
                previewOption.eq(index).removeClass('active');
                $this.find('option').eq(index).prop('selected', false);
                
                triggerChange();
            } else {
                $self.addClass('selected');
                previewOption.eq(index).addClass('active');
                $this.find('option').eq(index).prop('selected', true);
                
                triggerChange();
            }
        });
        
        previewCloseButton.on('click', function (e) {
            e.stopPropagation();
            var currentPreviewOption = $(this).closest('.selected-value');
            var index = currentPreviewOption.index();
            
            currentPreviewOption.removeClass('active');
            listOption.eq(index).removeClass('selected');
            $this.find('option').eq(index).prop('selected', false);
            
            triggerChange();
        });
        
        buttonClearAll.on('click', function (e) {
            e.stopPropagation();
            listOption.removeClass('selected');
            previewOption.removeClass('active');
            $this.find('option:selected').prop('selected', false);
            
            triggerChange();
        });
        
        // Returns number of selected items
        var triggerChange = function () {
            $this.change();
            if ($this.find('option:selected').length > 0)
                selectedOptions.removeClass('empty');
            else
                selectedOptions.addClass('empty');
            
            console.log($this.val());
        };
    });
}());