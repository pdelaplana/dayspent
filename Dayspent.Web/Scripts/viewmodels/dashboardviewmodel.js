function DashboardViewModel() {
    var self = this;

    self.weekdays = [];

    //
    // public observables
    //
    self.selectedPeriod = ko.observable();

    //
    // operations
    //
    self.showWeeklyChart = function (data, event) {

        var repository = new ActivityRepository();
        repository.period.from = moment().startOf('week');
        repository.period.to = moment().endOf('week');

        self.selectedPeriod(moment(repository.period.from).format('D') + ' - ' + moment(repository.period.to).format('D') + ' ' + moment(repository.period.from).format('MMMM'));


        // build weekdays array
        var currentDate = moment(repository.period.from);
        for (i = 0; i < 7; i++) {
            self.weekdays.push(new WeekDay(moment(currentDate).weekday(), moment(currentDate).format('ddd l'), 0));
            currentDate = moment(currentDate).add(1, 'day');
        }

        repository.get().done(function (result) {
            var found = false;

            $.each(result, function (i, activity) {
                var day = moment(activity.startDate).weekday();
                if (self.weekdays.length > 0) {
                    $.each(self.weekdays, function (i, weekday) {
                        if (weekday.day == day) {
                            weekday.timeSpentInMins = weekday.timeSpentInMins + activity.timeSpentMins;
                            found = true;
                            return false;
                        }
                    })
                    if (!found)
                        self.weekdays.push(new WeekDay(day, activity.dateGroup, activity.timeSpentMins));
                } else {
                    self.weekdays.push(new WeekDay(day, activity.dateGroup, activity.timeSpentMins));
                }
            });

            var chart = new WeeklyChart(self.weekdays);

        })


    }

    self.showMonthlyChart = function (data, event) {

    }

  

}


function WeekDay(day, date, timeSpentInMins){

    var self = this;

    self.day = day;
    self.date = date;
    self.timeSpentInMins = timeSpentInMins;

}

function MonthlyChart() {

    var chartData = {
        labels: [],
        datasets: []
    };

    var chartOptions = {
        scaleLabel: "<%= value %> hrs"
    }

    var labels = [], dataSeries = [];
    $.each(data, function (index, weekday) {
        labels.push(weekday.date);
        dataSeries.push(Math.round((weekday.timeSpentInMins / 60) * 100) / 100);
    })

}

function WeeklyChart(data) {
    var self = this;

    var chartData = {
        labels: [],
        datasets: []
    };

    var chartOptions = {
        scaleLabel : "<%= value %> hrs"
    }

    var labels = [], dataSeries = [];
    $.each(data, function (index, weekday) {
        labels.push(weekday.date);
        dataSeries.push(Math.round((weekday.timeSpentInMins / 60) * 100) / 100);
    })


    chartData.labels = labels;
    chartData.datasets.push({
        label: "Time Spent",
        fillColor: "rgba(151,187,205,0.5)",
        strokeColor: "rgba(151,187,205,0.8)",
        highlightFill: "rgba(151,187,205,0.75)",
        highlightStroke: "rgba(151,187,205,1)",
        data: dataSeries
    });

    // Get context with jQuery - using jQuery's .get() method.
    var ctx = $("#myChart").get(0).getContext("2d");
    // This will get the first returned node in the jQuery collection.
    return new Chart(ctx).Bar(chartData, chartOptions);


}