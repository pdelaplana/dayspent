function TimerDialog(statusReportItem) {
    var timer = null,
        seconds = 0,
        self = this;

    function formatTimeSpent(timeSpentSecs) {
        function pad(str, max) {
            str = str.toString();
            return str.length < max ? pad("0" + str, max) : str;
        }

        if (timeSpentSecs == null || timeSpentSecs == 0)
            return '0h 00m 00s';

        var hour = Math.floor((timeSpentSecs / 60) / 60);
        var mins = Math.floor(timeSpentSecs / 60) % 60;
        var secs = timeSpentSecs % 60;
        return hour + "h " + pad(mins, 2) + "m " + pad(secs, 2) + "s";
    }

    function startTimer() {
        var updateTime = function () {
            seconds = seconds + 1;
            self.timeSpentInSecs(Math.floor(seconds));
            self.timeSpent(formatTimeSpent(seconds));
        }
        seconds = self.timeSpentInSecs();
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
        self.timeSpentInSecs(0);
        self.timeSpent('0h 00m 00s');
    }

    function resetValues() {
        seconds = 0;
        resetTimer();
    }

    self.startDateTime = ko.observable(moment().format('lll'));
    self.timeSpentInSecs = ko.observable(statusReportItem.timeSpentInSecs());
    self.timeSpent = ko.observable(formatTimeSpent(statusReportItem.timeSpentInSecs()));
    

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

    self.close = function () {
        self.timer.stop();
        statusReportItem.timeSpentInSecs(Math.floor(self.timeSpentInSecs()));
        statusReportItem.timeSpent(statusReportItem.timeSpentFormatted());
        statusReportItem.addTimeSpent();
        $.unblockUI();
    }
    
    
}