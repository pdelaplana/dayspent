/**
 * uploadButton - create a button for file uploads 
 * 
 */
ko.bindingHandlers.uploadButton = {
    init: function (element, valueAccessor, allBindingsAccessor) {
        var bindings = allBindingsAccessor();
        var settings = bindings.settings;
        var inputFile = $('<input/>').attr('type', 'file').insertAfter($(element)).hide();
        inputFile.change(function () {
            var file = this.files[0];
            if (ko.isObservable(valueAccessor())) {
                valueAccessor()(file);
            }
            settings.upload();
        });
        $(element).click(function () {
            inputFile.click();
        })
    },
    update: function (element, valueAccessor, allBindingsAccessor) {
        var file = ko.utils.unwrapObservable(valueAccessor());
        var bindings = allBindingsAccessor();
        var settings = bindings.settings;
        if (settings.fileObjectURL && ko.isObservable(settings.fileObjectURL)) {
            var oldUrl = settings.fileObjectURL();
            if (oldUrl) {
                windowURL.revokeObjectURL(oldUrl);
            }
            settings.fileObjectURL(file && windowURL.createObjectURL(file));
        }

        if (settings.fileBinaryData && ko.isObservable(settings.fileBinaryData)) {
            if (!file) {
                settings.fileBinaryData(null);
            } else {
                settings.fileName(file.name);
                settings.fileType(file.type);

                var reader = new FileReader();
                reader.onload = function (e) {
                    settings.fileBinaryData(e.target.result);

                    /*
                    var result = e.target.result || {};
                    var resultParts = result.split(',');
                    if (resultParts.length === 2) {
                        bindings.fileBinaryData(resultParts[1]);
                        //bindings.fileType(resultParts[0]);

                    }
                    */

                };
                reader.readAsArrayBuffer(file);
                //reader.readAsText(file);
                //reader.readAsDataURL(file);
            }
        }
    }

}

/**
 * timeago 
 * 
 */
ko.bindingHandlers.timeago = {
	init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
		var bindings = allBindings(),
            observable = valueAccessor();

		function updateTimeAgo() {
			$(element).text(moment.utc(ko.utils.unwrapObservable(observable())).fromNow())
		}

		setInterval(updateTimeAgo, 1000);

		$(element).attr('title', ko.utils.unwrapObservable(observable()));
		updateTimeAgo();

	}
}


/**
 * convert date to local
 * 
 */
ko.bindingHandlers.localDate = {
	update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
		var bindings = allBindings(),
            observable = valueAccessor();
		$(element).text(moment(moment.utc(this).toDate()).format('ll'));
	}
}

/**
 * binding handler to convert markdown content to html using MarkdownDeep
 * 
 */
ko.bindingHandlers.markdownToHtml = {
    update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var bindings = allBindings(),
            observable = valueAccessor(),
            markdown = new MarkdownDeep.Markdown();

        $(element).html(markdown.Transform(observable()).replace(/(^|\W)(#[a-z\d][\w-]*)/ig, '$1<span class="link">$2</span>'));
        
    }
}

/**
 * binding handler to show popModal dialog
 * 
 */
ko.bindingHandlers.popModal = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var bindings = allBindings(),
            value = ko.utils.unwrapObservable(valueAccessor()),
            enabled = bindings.enabled || true,
            options = $.extend({
                html: '',
                placement: 'bottomLeft',
                showCloseBut: true,
                onDocumentClickClose: false,
                onOkBut: function () { },
                onCancelBut: function () { },
                onLoad: function () { },
                onClose: function () {
                    $(element).removeClass('popModalOpen');
                }
            }, bindings.popModalOptions);

        if (enabled) {
            $(element).click(function (event) {
                if ($(this).hasClass('popModalOpen')) {
                    $(this).removeClass('popModalOpen');
                    $(this).popModal('hide');

                } else {
                    $(this).popModal(options);

                    ko.applyBindings(bindingContext.$data, $(element).next('.popModal').get(0));
                    $(this).addClass('popModalOpen');

                }
                event.preventDefault();
                event.stopPropagation();
            })
        }

    }
};


/**
 * binding handler to show jquery calender inside a popModal dialog
 * 
 */
ko.bindingHandlers.popModalCalendar = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var bindings = allBindings(),
            enable = ko.utils.unwrapObservable(valueAccessor()),
            observable = valueAccessor(),
            options = $.extend({
                html: '<div class="popModal_calendar"></div><div class="padding10 nbp"><a href="" class="button">Clear</a></div>',
                //html: div.get(0),
                placement: bindings.placement != null ? bindings.placement : 'bottomLeft',
                showCloseBut: true,
                onDocumentClickClose: true,
                onOkBut: function () { },
                onCancelBut: function () { },
                onLoad: function (event) {

                    var calendar = $('.popModal_calendar').datepicker();
                    var clearBtn = $(calendar).next('div').find('.button');
                    if (bindings.noClearButton) clearBtn.hide();

                    var funcOnClear = function (event) {
                        observable(null);
                        if (bindings.saveValue != null)
                            bindings.saveValue(observable());
                        event.preventDefault();
                    };

                    var funcOnSelectdate = function () {

                        if (observable != null) {

                            var current = $(calendar).datepicker("getDate");
                            if (current != null)
                                observable(current);
                            else
                                observable(null);

                            if (bindings.saveValue != null)
                                bindings.saveValue(observable());

                        }

                        $(element).removeClass('popModalOpen');
                        $(element).popModal('hide');

                    }
                    //options.onSelect = funcOnSelectdate;
                    ko.utils.registerEventHandler(calendar, 'change', funcOnSelectdate);
                    ko.utils.registerEventHandler(clearBtn, 'click', funcOnClear);

                    $(calendar).datepicker('setDate', moment(observable()).toDate());

                },
                onClose: function () {
                    $(element).removeClass('popModalOpen');
                }
            }, bindings.popModalOptions);


        $(element).click(function (event) {
            if ($(this).hasClass('popModalOpen')) {
                $(this).removeClass('popModalOpen');
                $(this).popModal('hide');

            } else {
                $(this).popModal(options);
                $(this).addClass('popModalOpen');
            }
            event.preventDefault();
            event.stopPropagation();

        })

    },
    update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var calendar = $(element).next('.popModal').find('.popModal_calendar'),
            value = ko.utils.unwrapObservable(valueAccessor()),
            current = calendar.datepicker('getDate');

        $(calendar).datepicker('setDate', moment(value).toDate());


    }
};

/**
 * binding handler to bind jquery datepicker to an input field
 * 
 */
ko.bindingHandlers.datepicker = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        //initialize datepicker with some optional options
        var bindings = allBindings(),
            options = allBindings().datepickerOptions ||
            {};
        
        var funcOnSelectdate = function () {
            var observable = valueAccessor();
            //var observable = ko.utils.unwrapObservable(valueAccessor());
            if (observable != null) {

                var current = $(element).datepicker("getDate");
                if (current != null)
                    observable(current);
                else
                    observable(null);

                if (bindings.saveValue != undefined)
                    bindings.saveValue(observable());

            }
        }
        options.onSelect = funcOnSelectdate;
        $(element).datepicker(options);
        //ko.utils.registerEventHandler(element, 'change', funcOnSelectdate);

        //handle disposal (if KO removes by the template binding)
        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            $(element).datepicker("destroy");
        });

        
        // allow  
        $(element).next('.btn-date').click(function () {
            $(element).datepicker('show');
        })

    },
    update: function (element, valueAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor()),
            current = $(element).datepicker('getDate');

        if (!moment(value).isSame(moment(current))) {
            $(element).datepicker('setDate', moment(value).toDate());
        }
        
    }
};


/**
 * binding handler to bind jquery datepicker to an input field
 * 
 */
ko.bindingHandlers.timepicker = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        //initialize datepicker with some optional options
        var observable = valueAccessor(),
            bindings = allBindings(),
            options = allBindings().datepickerOptions ||
            {};
        $(element)
            .inputmask( 'h:s t' )
            .timepicker()
            .on('changeTime', function () {

                //moment(observable())
                //alert(moment($(this).val(), 'h:mm a').format('H:mm'));
                //alert(moment($(this).val(), 'h:mm a').format('H'));
                var hour = moment($(this).val(), 'h:mm a').format('H'),
                    minute = moment($(this).val(), 'h:mm a').format('mm');

                observable(moment(observable()).hour(hour).minute(minute).toDate());
                
            });

    },
    update: function (element, valueAccessor) {
        var observable = valueAccessor();
        $(element).timepicker('setTime', moment(observable()).toDate());
    }
}


/**
 * binding handler to bind tagit
 * 
 */
ko.bindingHandlers.tagit = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var bindings = allBindings(),
            observable = valueAccessor();

        var options = $.extend({
            initialTags: null,
            minLength: 0,
            triggerKeys: ['enter', 'tab'],
            enabled: true,
            tagSource: function (request, response) {
                $.ajax({
                    url: bindings.tagitOptions.sourceUrl,
                    type: 'GET',
                    dataType: 'json',
                    data: { name: request.term },
                    success: function (data) {
                        response($.map(data, function (name, val) {
                            return { label: name, value: name, id: val }
                        }))
                    }
                })
            },
            tagsChanged: function (tagValue, action, element) {
                if (action == 'added') {
                    observable.push(tagValue);
                    if (bindings.tagitOptions.addTag != null) {
                        bindings.tagitOptions.addTag(tagValue);
                    }
                } else if (action == 'popped') {
                    observable.remove(tagValue);
                    if (bindings.tagitOptions.removeTag != null) {
                        bindings.tagitOptions.removeTag(tagValue);
                    }

                }
            }
        }, bindings.tagitOptions);

        $(element).tagit(options);

    },
    update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var data = ko.utils.unwrapObservable(valueAccessor());
        //$(element).tagit('reset');
        $(element).tagit('fill', data);
    }
}


/**
 * custom binding handler to bind animation to knockout's visible binding
 * 
 */
ko.bindingHandlers.slideVisible = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var bindings = allBindings(),
            observable = valueAccessor();
    },
    update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var value = valueAccessor();
        ko.unwrap(!ko.utils.unwrapObservable(value)) ? $(element).slideUp() : $(element).slideDown();
    }
}

/**
 * custom binding handler to bind timer
 * 
 */
ko.bindingHandlers.timer = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var bindings = allBindings(),
            observable = valueAccessor();

        var timeIs = function () {
            if ((bindings.stopped == undefined) || (bindings.stopped != undefined && !bindings.stopped()))
                observable(observable() + 1);
            bindings.value($.utils.formatTimeSpent(observable()));
            
        }
        setInterval(timeIs, 60000);
        
    },
    update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var value = valueAccessor();
        
    }
}


/**
 * custom binding handler to add hover effects
 * 
 */
ko.bindingHandlers.hover = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var bindings = allBindings(),
            observable = valueAccessor(),
            prepend = $('<div class="grip">&nbsp;</div>');

        $(element).hover(
            function () { $(this).prepend(prepend) },
            function () { prepend.remove() }
            );
        
    },
    update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var value = valueAccessor();

    }
}

/**
 * custom binding handler to add autosize plugin to textarea
 * 
 */
ko.bindingHandlers.autosize = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        $(element).autosize();
    }
}

/**
 * custom binding handler to activate metro ui dropdown
 * 
 */
ko.bindingHandlers.dropdown = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        //$(element).next('ul[role=dropdown]').dropdown();
        $(element).dropdown();
    }
}

/**
 * custom binding handler to activate metro ui progress bar
 * 
 */
ko.bindingHandlers.progressbar = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var bindings = allBindings(),
            value = ko.utils.unwrapObservable(valueAccessor());

        $(element).progressbar({
            value: value,
            color: 'bg-green'
        });
    },
    update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var value = ko.utils.unwrapObservable(valueAccessor()),
            pb = $(element).progressbar();
        pb.progressbar('value', value);
    }
}

/**
 * custom binding handler to splitText into labels
 * 
 */
ko.bindingHandlers.splitText = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var bindings = allBindings(),
            text = ko.utils.unwrapObservable(valueAccessor()),
            result = '';
            
        $.each(text.split(','), function (index, value) {
            result += '<span class="label bg-blue fg-white">'+value+'</span> '
        })

        $(element).html(result);
    }
}

/**
 * custom binding handler to resize height of element to window
 * 
 */
ko.bindingHandlers.resize = {
    update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        $(element)
            .css('overflow', 'auto')
            .css('height', '90%')
            .height($(window).height() - 100);
    }
}

/**
 * custom binding handler to show a loading image 
 * 
 */
ko.bindingHandlers.loading = {
    update: function (element, valueAccessor, allBindingsAccessor) {
        var value = valueAccessor(), allBindings = allBindingsAccessor();
        var valueUnwrapped = ko.utils.unwrapObservable(value);

        if (valueUnwrapped == true)
            $(element).show(); // Make the element visible
        else
            $(element).hide();   // Make the element invisible
    }
};
