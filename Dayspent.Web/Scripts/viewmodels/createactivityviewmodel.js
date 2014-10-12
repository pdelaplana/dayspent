function CreateActivityViewModel() {
    var timer = null,
        self = this;

    var seconds = 0;

    function formatTimeSpent(timeSpentSecs){
        function pad(str, max) {
            str = str.toString();
            return str.length < max ? pad("0" + str, max) : str;
        }

        if (timeSpentSecs == null || timeSpentSecs == 0)
            return '0m';

        var hour = Math.floor((timeSpentSecs / 60) / 60);
        var mins = Math.floor(timeSpentSecs / 60) % 60;
        var secs = timeSpentSecs % 60;
        return hour + "h " + pad(mins, 2) + "m "+ pad(secs, 2)+"s";
    }

    function startTimer() {
        var updateTime = function() {
            seconds = seconds + 1;
            self.timeSpentInMins(Math.floor(seconds));
            self.timeSpent(formatTimeSpent(seconds));
        }
        self.timer.enabled(true);
        timer = setInterval(updateTime, 1000);
    }

    function stopTimer() {
        clearInterval(timer);
        self.timer.enabled(false);
    }

    function resetTimer() {
        seconds = 0;
        stopTimer();
        self.timeSpentInMins(0);
        self.timeSpent('0h 00m 00s');
    }

    function resetValues() {
        seconds = 0;
        self.description('');
        self.startDate(moment().toDate());
        resetTimer();
        self.tags([]);
    }

    //
    // observables
    //
    self.description = ko.observable();
    self.startDate = ko.observable(moment());
    self.timeSpent = ko.observable('0h 00m 00s');
    self.timeSpentInMins = ko.observable(0);
    self.tags = ko.observableArray();

    // state observables
    self.captureMode = ko.observable('timer');
    self.adding = ko.observable(false);

    //
    // subscriptions
    //
    self.adding.subscribe(function (newValue) {
        if (newValue)
            startTimer();
        else
            stopTimer();
    })

    self.captureMode.subscribe(function (newValue) {
        if (newValue == 'timer') {
           
        } else if (newValue == 'manual') {
            
        }
    })

    self.timer = {
        enabled: ko.observable(false),
        start: function () {
            startTimer();
        },
        stop: function () {
            stopTimer();
        },
        reset: function () {
            resetTimer();
        }
    }

    // 
    // operations
    //

    self.create = function () {

        if (self.description() == '') return;

        app.ui.block();

        var repository = new ActivityRepository();
        repository.timelineId = $.timeline.stream.timelineId();
        repository.description = self.description();
        repository.startDate = moment(self.startDate()).toJSON();
        repository.timeSpent = self.timeSpent();
        repository.tags = self.tags();
        repository.create().done(function (result) {

            // check if the new activity falls within period filters
            if (moment(result.data.startDate).isAfter(moment($.timeline.stream.periodFilters.from()))
                && moment(result.data.startDate).isBefore(moment($.timeline.stream.periodFilters.to()))){

                $.timeline.stream.activities.insert(new ActivityViewModel(result.data), 0);
            }

            resetValues();
            app.ui.unblock();
            $.Notify.show("A new time entry has been added.")

        })

    }


    self.cancel = function () {
        resetValues();
        self.adding(false);

    }


    self.toggleCaptureMode = function () {
        if (self.captureMode() == 'timer'){
            self.captureMode('manual');
            self.timeSpent('');
            self.timeSpentInMins(0);
            stopTimer();

        }
        else {
            self.captureMode('timer');
            self.timeSpent('0h 00m 00s');
            self.timeSpentInMins(0);
            startTimer();
        }
            

    }

  
}