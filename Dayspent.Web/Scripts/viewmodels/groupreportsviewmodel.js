function GroupReportsViewModel(data) {
    var self = this;


    self.reportItems = ko.observableArray([]);
    self.dateSelected = ko.observable();

    //
    // computed observabes
    //
    self.reportItems.byDate = ko.computed(function () {
        var dates = ko.utils.arrayMap(self.reportItems(), function (item) {
            return item.reportDate()
        });
        return ko.utils.arrayGetDistinctValues(dates).sort(function (a, b) {
            return moment(a).isAfter(b) ? -1 : 1;
        });
        
    })

    self.itemsByCategories = ko.computed(function () {
        var reportCategories = [],
            dateSelected = self.dateSelected(),
            items = ko.utils.arrayFilter(self.reportItems(), function (item) {
                return (moment(item.reportDate()).diff(dateSelected, 'days') == 0);
            });
        
        $.each(app.cache.reportCategories(), function (i, category) {

            var itemsGroup = new ItemsGroup();
            itemsGroup.statusReportCategoryId(category.statusReportCategoryId());
            itemsGroup.code(category.code());
            itemsGroup.description(category.description());

            var itemsArray = ko.utils.arrayFilter(items, function (reportItem) {
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

    //
    // do init stuff here
    // 

    // populate statusReports observable array
    $.each(data.reportItems, function (index, reportItem) {
        self.reportItems.insert(new StatusReportItemViewModel(reportItem));
    })


}

function GroupReport(date) {
    var self = this;

    self.reportDate = ko.observable(date);
    self.reportItems = ko.observable();

}

function GroupReportItem() {
    var self = this;


}