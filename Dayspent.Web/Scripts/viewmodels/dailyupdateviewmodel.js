function DailyUpdateViewModel(data) {
    var self = this;
    self.reportDate = ko.observable(moment().startOf('day'));
    self.userSummaries = ko.observableArray();

    
    self.totals = {
        inProgressWork: ko.computed(function () {
            return self.userSummaries.sum('inProgressWork');
        }),
        completedWork: ko.computed(function () {
            return self.userSummaries.sum('completedWork');
        }),
        notStartedWork: ko.computed(function () {
            return self.userSummaries.sum('notStartedWork');
        }),
        impediments: ko.computed(function () {
            return self.userSummaries.sum('impediments');
        }),
        all: ko.computed(function () {
            
            return parseInt(self.userSummaries.sum('inProgressWork')) 
                + parseInt(self.userSummaries.sum('completedWork'))
                + parseInt(self.userSummaries.sum('notStartedWork'));
            
        })


    }

    //
    // charting data
    //
    function generateChartData(chartFor, colorCode) {
        var total = self.totals.all(),
            actual = self.userSummaries.sum(chartFor) == '' ? 0 : self.userSummaries.sum(chartFor) ,
            other = (total == 0 ? 1 : total) - actual;

        return [
            {
                value: other,
                color: "#ddd",
                highlight: "#ddd",
                label: "Unspent"
            },
            {
                value: actual,
                color: colorCode,
                highlight: colorCode,
                label: "Spent"
            }

        ]

    }
    
    self.dataForInProgressChart = ko.computed(function () {

        return generateChartData('inProgressWork', "#60a917")

    }).extend({ rateLimit: { method: "notifyWhenChangesStop", timeout: 0 } })

    self.dataForCompletedChart = ko.computed(function () {

        return generateChartData('completedWork', "#00aff0")

    }).extend({ rateLimit: { method: "notifyWhenChangesStop", timeout: 0 } })
    
    self.dataForNotStartedChart = ko.computed(function () {

        return generateChartData('notStartedWork', "#555555")

    }).extend({ rateLimit: { method: "notifyWhenChangesStop", timeout: 0 } })

    self.dataForImpedimentsChart = ko.computed(function () {

        return generateChartData('impediments', "#e3c800")

    }).extend({ rateLimit: { method: "notifyWhenChangesStop", timeout: 0 } })

    self.dataForProblemsChart = ko.computed(function () {

        return generateChartData('redFlags', "#e51400")

    }).extend({ rateLimit: { method: "notifyWhenChangesStop", timeout: 0 } })

    //
    // operations
    //
    self.changeDate = function () {
        $.ajax({
            url: '/api/dashboard/summaries/',
            type: 'post',
            dataType: 'json',
            data:{ 
                reportDate: moment.utc(self.reportDate()).toJSON()
            },
            success: function (result) {
                self.userSummaries.removeAll();
                $.each(result, function (index, userSummary) {
                    self.userSummaries.push(new UserSummaryViewModel(userSummary));
                })

            }

        });
    }

    self.changeDate();
   

}