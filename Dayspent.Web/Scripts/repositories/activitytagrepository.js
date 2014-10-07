function ActivityTagRepository() {
    var self = this;
    self.name = null;
    self.activityId = null;

    self.create = function () {
        return $.ajax({
            url: '/api/activity/'+self.activityId+'/tags',
            type: 'post',
            data: {
                ActivityId: self.activityId,
                TagName: self.name
            }
        });
    }

    self.remove = function () {
        return $.ajax({
            url: '/api/activity/' + self.activityId+'/tags/'+self.name,
            type: 'delete'
        });

    }
}