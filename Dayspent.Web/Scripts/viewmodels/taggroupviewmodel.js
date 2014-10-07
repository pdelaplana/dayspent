function TagGroupViewModel(tagGroup) {
    var self = this;

    self.name = ko.observable(tagGroup);
    self.activities = ko.observableArray();
    self.timeSpent = ko.computed(function () {
        var totalTime = 0;
        ko.utils.arrayForEach(self.activities(), function (activity) {
            if (activity.visible())
                totalTime = totalTime + activity.timeSpentMins();
        })

        return $.utils.formatTimeSpent(totalTime);
    }).extend({ rateLimit: { timeout: 700, method: 'notifyWhenChangesStop' } })

    self.add = function (activity) {


    }

    self.remove = function (activity) {


    }
}

