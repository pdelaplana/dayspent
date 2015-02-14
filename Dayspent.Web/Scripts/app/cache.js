function Cache() {
    var self = this,
        reportCategories = { data : [], loaded : false};


    self.reportCategories = function () {
        if (!reportCategories.loaded) {
            // load the array
            $.getJSON('/api/categories', function (result) {
                $.each(result, function (i, category) {
                    reportCategories.data.push(new StatusReportCategoryViewModel(category))
                });

            })
            reportCategories.loaded = true;
        }
        return reportCategories.data;

    };



}