//
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
    app.ui.addPart('appNavigationBar', new AppNavigationBar()).bindTo('#AppNavigationBar');
    app.ui.setWindowTitle('Home');

    app.start('app/#/timeline');

})
