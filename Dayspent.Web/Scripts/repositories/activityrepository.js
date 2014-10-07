function ActivityRepository() {
    var self = this;

    self.timelineId = null;
    self.activityId = null;
    self.description = null;
    self.startDate = null;
    self.endDate = null;
    self.tags = null;
    self.timeSpent = null;
    self.timeSpentInMins = null;

    self.period = {
        from: null,
        to: null
    }
   
    self.get = function () {
        var getData = {};
        getData.From = self.period.from != null ? self.period.from.toJSON() : null;
        getData.To = self.period.to != null ? self.period.to.toJSON() : null;

        return $.ajax({
            url: '/api/activities',
            type: 'get',
            data : getData
        })
    }

    self.create = function () {
        return $.ajax({
            url: '/api/activities',
            type: 'post',
            data: {
                TimelineId: self.timelineId,
                Description: self.description,
                StartDate: self.startDate,
                Tags: self.tags || null,
                TimeSpent: self.timeSpent
            }
        });
    }

    self.update = function () {
        return $.ajax({
            url: '/api/activities/'+self.activityId,
            type: 'put',
            data: {
                ActivityId: self.activityId,
                Description: self.description,
                StartDate: self.startDate,
                TimeSpent: self.timeSpent
            }
        });
    }

    self.remove = function () {
        return $.ajax({
            url: '/api/activities/' + self.activityId,
            type: 'delete'
        });
    }


}