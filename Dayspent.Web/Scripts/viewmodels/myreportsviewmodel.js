function MyReportsViewModel(data) {
    var self = this;

    self.reportCategories = ko.observableArray([]);
    app.reportCategories = self.reportCategories; // attach to app object to make it global

    self.statusReports = ko.observableArray([]);
    self.statusReports.selected = ko.computed(function () {
        return ko.utils.arrayFirst(self.statusReports(), function (statusReport) {
            return statusReport.selected();
        })
    })
    self.selectStatusReport = function (statusReport) {
        $.each(self.statusReports(), function (i, report) {
            report.selected(false);
        })
        statusReport.selected(true);
    }
    self.sortedStatusReportsByDate = ko.computed(function () {
        return self.statusReports().sort(function (a,b) {
            return moment(a.reportDate()).isAfter(b.reportDate()) ? -1 : 1;
        })
    })
    app.statusReports = self.statusReports;



    //
    // do init stuff here
    //

    // populate reportCategories observable array and attach to app object
    $.each(data.reportCategories, function (index, category) {
        self.reportCategories.push(new StatusReportCategoryViewModel(category));
    })
    

    // populate statusReports observable array
    $.each(data.statusReports, function (index, statusReport) {
        self.statusReports.insert(new StatusReportViewModel(statusReport));
    })

    // select the first status report
    if (self.statusReports.selected() == null) {
        self.statusReports()[0].selected(true);
    }

}