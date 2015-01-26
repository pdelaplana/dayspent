﻿//
// create application object
//
var app = new Application({
    appTitle: 'Dayspent',
    appContainer: '#application-container'
});


$(function () {
    //
    // global ajax settings
    //
    $.ajaxSetup({
        // Disable caching of AJAX responses
        cache: false,
        error: function (err, type, httpStatus) {
            if ((err.status == '403') || (err.status == '401') || (err.status == '0')) {
                window.location.reload();
            } else {
                var dialog = new ErrorDialog(err);
                dialog.open();
                console.log(err.status + " - " + err.responseText + " - " + httpStatus);
            }
        }
    });

    // 
    // block ui defaults
    // 
    $.blockUI.defaults.overlayCSS.opacity = .80;
    $.blockUI.defaults.overlayCSS.backgroundColor = '#eeeeee';

    //
    // set datepicker defaults
    //
    $.datepicker.setDefaults($.datepicker.regional['en-GB']);

    // get authenticated user details from cookie
    var authenticatedUser = $.cookie("AuthenticatedUser");
    if (authenticatedUser != null)
        authenticatedUser = ko.utils.parseJson(authenticatedUser)
    app.user.userId = authenticatedUser.id;
    app.user.userName = authenticatedUser.userName;
    app.user.fullName = authenticatedUser.fullName;

    // add parts
    app.ui.addPart('appNavigationBar', new AppNavigationBar()).bindTo('#AppNavigationBar');
    app.ui.addPart('appSideBar', new AppSideBar()).bindTo('#AppSideBar');
    app.ui.setWindowTitle('Home');

    app.start('app/#/dashboard');

})

app.utils = {
    formatTime: function (timeSpentMins) {
        function pad(str, max) {
            str = str.toString();
            return str.length < max ? pad("0" + str, max) : str;
        }

        if (timeSpentMins == null || timeSpentMins == 0)
            return '0m';

        var hour = Math.floor(timeSpentMins / 60);
        var mins = timeSpentMins % 60;
        return hour + "h " + pad(mins, 2) + "m ";
    }

}
