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

