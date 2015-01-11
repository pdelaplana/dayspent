var ItemPlaceHolder = {
    element: function (currentItem) {
        return $('<div class="list ol-transparent no-border no-padding bg-grayLighter" style="min-height:100px;"></div>')[0];
    },
    update: function (container, p) {
        return;
    }
}


function StatusReportViewModel(data) {
    var self = this;

    self.statusReportId = ko.observable(data.statusReportId);
    self.reportDate = ko.observable(data.reportDate);
    self.submittedDate = ko.observable(data.submittedDate);
   
    self.reportingUser = ko.observable(data.reportingUser);
    self.reportingUserFullName = ko.observable(data.reportingUserFullName);

    self.suggestions = ko.observable(data.suggestions);

    self.reportCategories = ko.observableArray([]);

    self.reportItems = ko.observableArray([]);

    //
    // state 
    //
    self.selected = ko.observable(false);

    //
    // computed variables
    //
    self.submitted = ko.computed(function () {
        return (self.submittedDate() != null)
    })
    self.itemsByCategories = ko.computed(function () {
        var reportCategories = [];
        $.each(app.reportCategories(), function (i, category) {

            var itemsGroup = new ItemsGroup();
            itemsGroup.statusReportCategoryId(category.statusReportCategoryId());
            itemsGroup.code(category.code());
            itemsGroup.description(category.description());

            var itemsArray = ko.utils.arrayFilter(self.reportItems(), function (reportItem) {
                return reportItem.statusReportCategoryId() == category.statusReportCategoryId()
            })
            ko.utils.arrayPushAll(itemsGroup.reportItems(), itemsArray);
            
            reportCategories.push(itemsGroup);
            
        })
        return reportCategories;
        
    })

    //
    // operations
    //
    self.repositionItems = function (arg) {
        // get container node
        var containerNode = $(this),
            category = ko.dataFor(this),
            item = arg.item,
            repository = new StatusReportRepository();

        repository.statusReportId = self.statusReportId();
        repository.statusReportCategoryId = category.statusReportCategoryId();
        repository.statusReportItemIds = category.reportItems.getArrayOfProperty('statusReportItemId');
        repository.reposition().done(function (result) {
            item.statusReportCategoryId(category.statusReportCategoryId());
        })
        
    }


    self.removeItem = function (item) {
        item.remove().success(function (result) {
            self.reportItems.remove(item);
        })
    }

    self.save = function () {
        var repository = new StatusReportRepository();
        repository.statusReportId = self.statusReportId();
        repository.reportDate = self.reportDate();
        repository.update().success(function (result) {
            app.ui.notify('Status report has been saved.')
        });
    }

    self.submit = function () {
        var repository = new StatusReportRepository();
        repository.statusReportId = self.statusReportId();
        repository.submittedDate = moment();
        repository.submit().then(function (result) {
            self.submittedDate(repository.submittedDate);
            self.selected(false);
            app.ui.notify('Status report has been submitted.');

            var statusReport = new StatusReportViewModel(result.data);
            statusReport.selected(true);
            app.statusReports.insert(statusReport);
        });
    }

    self.remove = function () {
        var repository = new StatusReportRepository();
        repository.statusReportId = self.statusReportId();
        repository.remove().then(function (result) {
            app.ui.notify('Status report has been deleted.');
            app.statusReports.remove(self);
        });

    }

    //
    // do init stuff here
    //
    $.each(data.statusReportItems, function (index,reportItem) {
        self.reportItems.push(new StatusReportItemViewModel(reportItem));
    })

    $.each(app.reportCategories(), function (i, category) {

        var itemsGroup = new ItemsGroup();
        itemsGroup.statusReportId(self.statusReportId());
        itemsGroup.statusReportCategoryId(category.statusReportCategoryId());
        itemsGroup.code(category.code());
        itemsGroup.description(category.description());

        var itemsArray = ko.utils.arrayFilter(self.reportItems(), function (reportItem) {
            return reportItem.statusReportCategoryId() == category.statusReportCategoryId()
        })
        ko.utils.arrayPushAll(itemsGroup.reportItems(), itemsArray.sort(function (a, b) {
            return (a.sequence() < b.sequence()) ? -1 : (a.sequence > b.sequence()) ? 1 : 0;

        }));

        self.reportCategories.push(itemsGroup);

    })

}

function ItemsGroup() {
    var self = this;
    self.statusReportId = ko.observable();
    self.statusReportCategoryId = ko.observable();
    self.code = ko.observable();
    self.description = ko.observable();
    self.reportItems = ko.observableArray([]);
    self.removeItem = function (item) {
        if (confirm('Please confirm that you wish to delete this entry?')) {
            item.remove().success(function (result) {
                self.reportItems.remove(item);
                app.ui.notify('Status report entry has been deleted.');
            })
        }
    }
}