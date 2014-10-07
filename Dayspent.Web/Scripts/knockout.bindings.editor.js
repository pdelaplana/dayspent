function MarkdownEditor(observable, viewModel) {
    var markdown = new MarkdownDeep.Markdown(),
        editorContainer = $('<div/>').addClass('markdown-editor').hide(),
        preview = $('<div/>').addClass('mdd_preview').hide(),
        toolbar = $('<div/>').addClass('mdd_toolbar'),
        textarea = $('<textarea/>').addClass('mdd_editor'),
        saveButton = $('<button/>').addClass('primary push-down-15').text('Save'),
        editButton = $('<button/>').addClass('push-down-15').text('Edit').hide(),
        previewButton = $('<button/>').addClass('push-down-15').text('Preview'),
        cancelButton = $('<button/>').addClass('link push-down-15').text('Cancel');

            
    // create the editor , with markdown and autosize
    editorContainer
        .append(preview)
        .append('<div class="subheader">January 2,1992</div>')
        .append(toolbar)
        .append(textarea)
        .append(saveButton).append('&nbsp;')
        .append(editButton).append('&nbsp;')
        .append(previewButton).append('&nbsp;')
        .append(cancelButton);

    $(textarea)
        .val(observable())
        .MarkdownDeep({
            SafeMode: false,
            ExtraMode:true,
            help_location: "/Content/mdd_help.html",
            disableTabHandling: true,
            disableAutoIndent: true,
            resizebar: false,
            cmd_img: function (ctx) {
                //alert('to be implemented');
            },
            cmd_link: function (ctx) {
                var selection = $(textarea).data('selection') || $(textarea).focus().getSelection();
                var str = '';
                if (selection.text != '') {
                    str = '<' + selection.text + '>';
                } 
                $(textarea).setSelection(selection.start, selection.end);
                $(textarea).replaceSelectedText(str);
            },
            onPreTransform: function (editor, markdown) {
                observable(markdown);
            }
        })
        .autosize();

    previewButton.click(function (event) {
        preview.html(markdown.Transform(observable()));
        textarea.fadeOut('slow', function () {
            preview.fadeIn();
            toolbar.hide();
            previewButton.hide();
            cancelButton.hide();
            editButton.show();
        });
    });

    editButton.click(function (event) {
        preview.fadeOut('slow', function () {
            toolbar.show();
            textarea.fadeIn().trigger('autosize.resize');
            previewButton.show();
            cancelButton.show();
            editButton.hide();
        });
    });

	/*
    // make the text area a dropzone
    var dropzone = new Dropzone($(textarea).get(0),{
        url: '/api/card/' + viewModel.cardId() + '/attachments/',
        method: 'post',
        previewsContainer: "#previews",
        clickable: $(toolbar).find('#mdd_img').get(0),
        dictResponseError: 'test error',
        init: function () {
            this.on('addedfile', function (file) {

            })
            this.on('complete', function (file) {
                this.removeFile(file);
            })
            this.on('success', function (file, result) {

                textarea.focus();

                var height = 100, width = 100;
                var selection = $(textarea).data('selection') || $(textarea).focus().getSelection();

                height = (height == undefined) ? 0 : height;
                width = (width == undefined) ? 0 : width;

                var str = '';
                if (selection.text != '') {
                    str = '[' + selection.text + ']';
                } else {
                    str = '[' + result.data.fileName + ']';
                }
                if (result.data.contentType.split('/')[0] == 'image')
                    str = '!' + str + '(/api/image/' + result.data.cardAttachmentId + ')';
                else
                    str = str + '(/api/file/' + result.data.cardAttachmentId + ')';

                $(textarea).setSelection(selection.start, selection.end);
                $(textarea).replaceSelectedText(str);
                observable($(textarea).val());

                //
                viewModel.attachmentsList.attachments.splice(0, 0, new AttachmentListItem(result.data));
                viewModel.attachmentCount(viewModel.attachmentsList.attachments().length);
                $.boardcontext.current.hub.notify.onCardAttachmentAdded(viewModel.boardId(), result.data, result.activityContext);

            })
            this.on('drop', function (event) {
                var e = event;
            });
        },
    });
	*/

    // assign current text selection to textarea so we canget access to it later
    $(textarea).click(function () {
        $(textarea).data('selection', $(this).getSelection());
    }).keyup(function () {
        $(textarea).data('selection', $(this).getSelection());
    })

    return {
        editor: editorContainer,
        textArea: textarea,
        button: {
            edit: editButton,
            save: saveButton,
            cancel: cancelButton
        },
        destroy: function () {
            //dropzone.destroy();
            editorContainer.remove();
        }
    }

}


/**
 * editable
 * - custom binding handler to make an element into an editable text  
 */
ko.bindingHandlers.editable = {
	init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
		var bindings = allBindings(),
            observable = valueAccessor(),
            markdown = new MarkdownDeep.Markdown();

		function CreateNewEditor() {
			var editorContainer = new MarkdownEditor(observable, viewModel);

			editorContainer.button.save.click(function (event) {
				if (editorContainer.textArea.val().length > 0) {
					observable(editorContainer.textArea.val());
					$(element).show();
					editorContainer.hide();
					editorContainer.destroy();
					editorContainer.textArea.val('');
					if (bindings.save != undefined)
					    bindings.save();
				}
			});

			editorContainer.button.cancel.click(function () {
			    editorContainer.editor.hide();
			    editorContainer.destroy();
			    $(element).show();
			})

			return editorContainer;

		}

		$(element).click(function () {

			$(element).hide();
			var editorContainer = CreateNewEditor();
			$(element).after(editorContainer.editor);

			editorContainer.editor.show();
			editorContainer.textArea.focus();
			editorContainer.textArea.trigger('autosize.resize');
			

		})


		

	},
	update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
		var observable = valueAccessor(),
            content = observable();
		

	}
}

/**
 * editable
 * - custom binding handler to make an element into an editable text  
 */
ko.bindingHandlers.mdTextArea = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var bindings = allBindings(),
            observable = valueAccessor(),
            markdown = new MarkdownDeep.Markdown();

        $(element)
            .val(observable())
            .MarkdownDeep({
                SafeMode: false,
                ExtraMode:true,
                help_location: "/Content/mdd_help.html",
                disableTabHandling: true,
                disableAutoIndent: true,
                resizebar: false,
                cmd_img: function (ctx) {
                    //alert('to be implemented');
                },
                cmd_link: function (ctx) {
                    var selection = $(textarea).data('selection') || $(textarea).focus().getSelection();
                    var str = '';
                    if (selection.text != '') {
                        str = '<' + selection.text + '>';
                    } 
                    $(textarea).setSelection(selection.start, selection.end);
                    $(textarea).replaceSelectedText(str);
                },
                onPreTransform: function (editor, markdown) {
                    observable(markdown);
                }
            })
            .autosize();


        // assign current text selection to textarea so we canget access to it later
        $(element).click(function () {
            $(element).data('selection', $(this).getSelection());
        }).keyup(function () {
            $(element).data('selection', $(this).getSelection());
        })
    }
}