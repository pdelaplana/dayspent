/**
 * editing area for status report item 
 * 
 */
function StatusReportItemEditingForm() {
    // compose the editing area
    var textarea = $('<textarea/>').attr('style', 'min-height:50px;height:50px').addClass(''),
        text = $('<input/>').attr('type', 'text').attr('placeholder', 'Link to a task or activity....').addClass('inline-block'),
        inputControl = $('<div/>').addClass('input-control textarea').append(textarea),
        inputControlText = $('<div/>').addClass('input-control text').append(text),
        //tagit = $('<ul/>').addClass('tagit transparent inline-block').width('400px'),
        //tagitContainer = $('<div/>').addClass('tags-container clearfix').width('100%').append(tagit),
        saveButton = $('<button>Save</button>').addClass('primary'),
        cancelButton = $('<button>Cancel</button>').addClass('link'),
        footerContainer = $('<div/>').addClass('bg-grayLighter padding10').append(saveButton).append(cancelButton),
        inputControlContainer = $('<div/>').addClass('').append(inputControlText).append(inputControl).append(footerContainer);
        //inputControlContainer = $('<div/>').addClass('').append(inputControl).append(footerContainer);

    var self = this;

    self.textArea = textarea;
    
    self.onSave = function () { alert('no process'); }
    self.onCancel = function () { alert('no process'); }

    self.init = function (element, valueAccessor, allBindings, viewModel, bindingContext) {

        // hook up event handlers
        saveButton.click(self.onSave);
        cancelButton.click(self.onCancel)

        //bind tagit container
        //ko.bindingHandlers.tagit.init($(tagit).get(0), valueAccessor, allBindings, viewModel, bindingContext);

        textarea.autosize().textcomplete([
        { // html
            mentions: ['test', 'apple'],
            match: /\B#(\w*)$/,
            search: function (term, callback) {
                $.ajax({
                    url: '/api/tags',
                    type: 'GET',
                    dataType: 'json',
                    data: { name: term },
                    success: function (data) {
                        callback($.map(data, function (name, val) {
                            return name;
                            //return { label: name, value: name, id: val }
                        }))
                    }
                })
            },
            index: 1,
            replace: function (mention) {
                return ' #' + mention + ' ';
            }
        }
        ]).focus();

        $(document).mouseup(function (e) {
            if (!inputControlContainer.is(e.target) // if the target of the click isn't the container...
                && inputControlContainer.has(e.target).length === 0) // ... nor a descendant of the container
            {
                inputControlContainer.hide();
                $(element).show();
            }
        });

    }

    self.hide = function () {
        inputControlContainer.hide()
    }
    self.show = function () {
        inputControlContainer.show();
        textarea.focus().autosize();
    }

    self.remove = function () {
        inputControlContainer.remove();
        textarea.val('');
    }

    self.appendTo = function (element) {
        $(element).append(inputControlContainer);
    }

    

}




/**
 * binding handler to show date period selection panel
 * 
 */
ko.bindingHandlers.datePeriodSelection = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
    	var bindings = allBindings(),
            observable = valueAccessor();
    	ko.renderTemplate(bindings.templateId, observable, {}, element, 'replaceNode');

    }
};

/**
 * binding handler to show date period selection panel
 * 
 */
ko.bindingHandlers.editActivityTemplate = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var bindings = allBindings(),
            model = valueAccessor();

        ko.renderTemplate(bindings.templateId, model, {}, element, 'replaceNode');

        $(document).mouseup(function (e) {
            var container = $('.editActivityTemplate');
            if (!container.is(e.target) // if the target of the click isn't the container...
                && container.has(e.target).length === 0) // ... nor a descendant of the container
            {
                //model.editing(false);
            }
        });

    }
};


/**
 * binding handler to add a status report item
 * 
 */
ko.bindingHandlers.addStatusReportItem = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var bindings = allBindings(),
            params = bindings.params,
            collection = valueAccessor(),
            tags = ko.observableArray(),
            newValueAccessor = function () { return tags };

      
        var saveFn = function () {
           
        }

        var form = new StatusReportItemEditingForm();

        form.onSave = function () {
            if (form.textArea.val().length == 0) return;
            var repository = new StatusReportItemRepository();
            repository.statusReportId = params.statusReportId;
            repository.statusReportCategoryId = params.statusReportCategoryId;
            repository.description = form.textArea.val();
            repository.tags = tags();
            repository.create().success(function (result) {
                var item = new StatusReportItemViewModel(result.data)
                collection.push(item);
                viewModel.parent.reportItems.push(item);
                form.hide();
                form.textArea.val('');
                $(element).show();
            });

        };
        form.onCancel = function () {
            form.hide();
            $(element).show();
        }
        
        form.hide();
        form.appendTo($(element).parent('div'));
        form.init(element, newValueAccessor, allBindings, viewModel, bindingContext);
        

        $(element).click(function () {
            $(this).hide();
            form.show();
        })
        

    }
};

/**
 * binding handler to do an inplace edit of a status report item
 * 
 */
ko.bindingHandlers.editStatusReportItem = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var bindings = allBindings(),
            params = bindings.params,
            item = valueAccessor(),
            newValueAccessor = function () { return viewModel.tags; }

        // get the content area
        var contentArea = $(element).parents('div.content-area');

        var form = new StatusReportItemEditingForm();
        form.onSave = function () {
            viewModel.description(form.textArea.val());
            viewModel.update().success(function (result) {
                viewModel.tags(result.data.tags);
                form.hide();
                $(contentArea).show();
            });

        };
        form.onCancel = function () {
            form.hide();
            $(contentArea).show();
        }
        form.hide();
        form.appendTo($(contentArea).parent('div'));
        form.init(contentArea, newValueAccessor, allBindings, viewModel, bindingContext);


        $(element).click(function (event) {
            $(contentArea).hide();
            form.show();
            form.textArea.val(viewModel.description());
            form.textArea.focus().trigger('autosize.resize');
            event.preventDefault();

        })
       
    }
};



/**
 * binding handler to do add time against a status report item
 * 
 */
ko.bindingHandlers.addTime = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var bindings = allBindings(),
            params = bindings.params,
            observable = valueAccessor();

        var saveFn = function () {
            viewModel.timeSpent(textbox.val());
            viewModel.addTimeSpent().success(function (result) {
                textbox.remove();
                viewModel.timeSpentInSecs(result.data.timeSpentInSecs);
                $(element).show();
            });
        }

        // get the content area
        var contentArea = $(element).parent('div');

        // build the editing area
        var textbox = $('<input type="text"/>');

        $(element).click(function (event) {

            $(element).hide();
            contentArea.append(textbox);
            textbox.val('').focus() // hook up event handlers
                .keypress(function(event){
                    if (event.which == 13) {
                        saveFn();
                    }
                
                });
            //ko.utils.registerEventHandler(saveButton, 'click', saveFn);
            event.preventDefault();

        })
        $(document).mouseup(function (e) {
            if (!textbox.is(e.target) // if the target of the click isn't the container...
                && textbox.has(e.target).length === 0) // ... nor a descendant of the container
            {
                textbox.remove();
                $(element).show();
            }
        });

    },
    update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var observable = valueAccessor();
        $(element).text(observable());
    }
};


/**
 * binding handler to add a status report item 
 * 
 */
ko.bindingHandlers.addStatusReportItemTemplate = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var bindings = allBindings(),
            data = valueAccessor(),
            renderedElement = null,
            model = new AddStatusReportItemTemplate();

        model.statusReportId(viewModel.statusReportId());
        model.statusReportCategoryId(viewModel.statusReportCategoryId());
        model.category(viewModel.description());
        model.save = function () {
            var repository = new StatusReportItemRepository();
            repository.statusReportId = model.statusReportId();
            repository.statusReportCategoryId = model.statusReportCategoryId();
            repository.description = model.description();
            return repository.create().success(function (result) {
                var item = new StatusReportItemViewModel(result.data)
                viewModel.reportItems.push(item);
                //viewModel.reportItems.push(item);
                $(renderedElement).find('[data-role=label]').show();
                $(renderedElement).find('[data-role=form]').hide();
            });
        }
        ko.renderTemplate(
            data.templateId,
            model,
            {
                afterRender: function (renderedElement) {
                    var label = $(renderedElement).find('div[data-role=label]').show(),
                        form = $(renderedElement).find('div[data-role=form]').hide();

                    renderedElement = renderedElement;
                    label.click(function () {
                        label.hide();
                        form.show();
                        form.find('textarea').autosize().focus();
                    })

                    $(document).mouseup(function (e) {
                        //var container = $('.AddStatusReportItem');
                        var container = $(renderedElement);
                        if (!container.is(e.target) // if the target of the click isn't the container...
                            && container.has(e.target).length === 0) // ... nor a descendant of the container
                        {
                            label.show();
                            form.hide();

                            model.activated(false);
                        }
                    });
                    
                }
            },
            element,
            'replaceNode');  

    }
};

/**
 * binding handler to open a modal timer
 * 
 */
ko.bindingHandlers.modalTimer = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var timer = $('#Timer'),
            observable = valueAccessor();
            
        $(element).click(function (event) {
            $.blockUI({ message: timer.html() });
            ko.applyBindings(new TimerDialog(viewModel), $('.blockUI div').get(0));
            event.preventDefault();
        })

    }
};

/**
 * binding handler to open a modal timer
 * 
 */
ko.bindingHandlers.linkToProject = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        // get the content area
        var contentArea = $(element).parent('div');

        // build the editing area
        var textbox = $('<input type="text" style="width:200px"/>'),
            contentLink = $(element);

        $(element).click(function (event) {

            $(element).hide();
            $(element).after(textbox);
            textbox.val('').focus() // hook up event handlers
                .keypress(function (event) {
                    if (event.which == 13) {
                        saveFn();
                    }

                });
            event.preventDefault();

        })
        $(document).mouseup(function (e) {
            if (!textbox.is(e.target) // if the target of the click isn't the container...
                && textbox.has(e.target).length === 0) // ... nor a descendant of the container
            {
                textbox.remove();
                $(element).show();
            }
        });

    }
};