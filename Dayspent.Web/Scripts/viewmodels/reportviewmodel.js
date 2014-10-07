function ReportViewModel() {
    var self = this;

    self.activities = ko.observableArray();
    self.tagGroups = ko.observableArray();

    //
    // tagGroups extenions
    //
    self.tagGroups.add = function (activity) {
        var tagGroup = self.tagGroups.findByProperty('name', activity.tagGroup());
        if (tagGroup == null) {
            tagGroup = new TagGroupViewModel(activity.tagGroup())
            self.tagGroups.push(tagGroup);
        }
        tagGroup.activities.push(activity);
    }

    self.tagGroups.remove = function (activity) {
        var tagGroup = self.tagGroups.findByProperty('name', activity.tagGroup());
        if (tagGroup != null) {
            var activity = tagGroup.activities.findByProperty('activityId', activity.activityId());
            if (activity != null) {
                tagGroup.activities.remove(activity);
            }
        }
    }



    self.activities.subscribe(function (changes) {

        ko.utils.arrayForEach(changes, function (change) {
            var activity = change.value;

            if (change.status == 'added') {

                self.tagGroups.add(activity);

            } else if (change.status == 'deleted') {
                
                self.tagGroups.remove(activity);
            }
        });

    }, null, 'arrayChange');

    //
    // load 
    //

    var repository = new ActivityRepository();
    repository.get().done(function (result) {
        $.each(result, function (index, activity) {
            self.activities.push(new ActivityViewModel(activity));
        });
    })

}