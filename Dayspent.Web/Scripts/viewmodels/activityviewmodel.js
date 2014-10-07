function ActivityViewModel(data) {
    var self = this;

    self.activityId = ko.observable(data.activityId);
    self.activityByUserId = ko.observable(data.activityByUserId);
    self.activityByUserFullName = ko.observable(data.activityByUserFullName);
    self.description = ko.observable(data.description);
    self.dateGroup = ko.observable(data.dateGroup);
    self.startDate = ko.observable(data.startDate);
    self.endDate = ko.observable(data.endDate);
    self.timeSpent = ko.observable(data.timeSpent);
    self.timeSpentMins = ko.observable(data.timeSpentMins);
    self.tagGroup = ko.observable(data.tagGroup);
    self.tags = ko.observableArray(data.tags);

    

    // state 
    self.visible = ko.observable(true);
    self.editing = ko.observable(false);

    // computed
    self.timeSpentMinsFormatted = ko.computed(function () {
        return $.utils.formatTimeSpent(self.timeSpentMins());
    }).extend({ rateLimit: 500 });

    // operations
    self.update = function () {        
        var repository = new ActivityRepository();
        repository.activityId = self.activityId();
        repository.description = self.description();
        repository.startDate = moment(self.startDate()).toJSON();
        repository.timeSpent = self.timeSpent();
        repository.update().done(function (result) {

            var activity = $.timeline.stream.activities.findByProperty('activityId', result.data.activityId);
            var oldDate = moment(activity.startDate()).toDate();
            activity.startDate(result.data.startDate);
            activity.description(result.data.description);
            activity.timeSpent(result.data.timeSpent);
            activity.timeSpentMins(result.data.timeSpentMins);

            $.timeline.stream.activities.remove(activity);
            $.timeline.stream.activities.push(activity);

            var currentDate = moment(result.data.startDate);
            if (!currentDate.isSame(oldDate)){
                // if date has changed remove and reinsert
                
            }
            self.editing(false);
            
        })
    }

    self.remove = function () {
        if (confirm('Continue delete?')) {
            var repository = new ActivityRepository();
            repository.activityId = self.activityId();
            repository.remove().done(function (result) {
                var activity = $.timeline.stream.activities.findByProperty('activityId', self.activityId());
                $.timeline.stream.activities.remove(activity);
            })
        }
    }


    self.addTag = function (tagName) {
        var repository = new ActivityTagRepository();
        repository.activityId = self.activityId();
        repository.name = tagName;
        repository.create().done(function (result) {
            self.tags.push(result.data.tagName);
        })
    }

    self.removeTag = function (tagName) {
        var repository = new ActivityTagRepository();
        repository.activityId = self.activityId();
        repository.name = tagName;
        repository.remove().done(function (result) {
            self.tags.remove(result.data.tagName);
        })
    }
}

