$(function () {

    $.ajaxSetup({
        cache: false
    });

    // Wire up the click event of any current or future dialog links
    $('.hubChangeLink').live('click', function () {
        var element = $(this);

        // Retrieve values from the HTML5 data attributes of the link        
        var dialogTitle = element.attr('data-dialog-title');
        var updateTargetId = '#' + element.attr('data-update-target-id');
        var updateUrl = element.attr('data-update-url');

        // Generate a unique id for the dialog div
        var dialogId = 'uniqueName-' + Math.floor(Math.random() * 1000);
        var dialogDiv = "<div id='" + dialogId + "'></div>";

        // Load the form into the dialog div
        $(dialogDiv).load(this.href, function () {
            $(this).dialog({
                modal: true,
                resizable: false,
                width: '460px',

                buttons: {
                    "Change": function () {
                        // Manually submit the form                        
                        var form = $('form', this);
                        $(form).submit(
                        //                          function () {
                        //                              $("input.submit").attr("disabled", "true");
                        //                          }
                        );
                    },
                    "Cancel": function () { $(this).dialog('close'); }
                }
            });
            $(".ui-dialog-titlebar-close").hide(); // a workaround to remove the x button
            $(".ui-widget-overlay").css({ 'opacity': '0.85' });
            // Enable client side validation
            //$.validator.unobtrusive.parse(this);

            // Setup the ajax submit logic
            //wireUpForm(this, updateTargetId, updateUrl);
        });

        return false;
    });


    // Wire up the click event of any current or future dialog links
    $('.dialogLink').live('click', function () {
        var element = $(this);
        var count = 0;
        // Retrieve values from the HTML5 data attributes of the link        
        var dialogTitle = element.attr('data-dialog-title');
        var updateTargetId = '#' + element.attr('data-update-target-id');
        var updateUrl = element.attr('data-update-url');

        // Generate a unique id for the dialog div
        var dialogId = 'uniqueName-' + Math.floor(Math.random() * 1000);
        var dialogDiv = "<div id='" + dialogId + "'></div>";
        $('#ajax-content').html(dialogDiv);
        // Load the form into the dialog div
        $("#" + dialogId).load(this.href, function () {
            $(this).dialog({
                modal: true,
                resizable: false,
                width: '500px',
                title: dialogTitle,
                buttons: {
                    "Save": function () {
                        // Manually submit the form                        
                        var form = $('form', this);
                        // if the form is not valid Unobtrusive should handle the validation no need to bother
                      //  if (form.valid()) {
                      //      count++;
                      //  } else {
                       //     return;
                       // }
                    //    if (count <= 1) {
                            $(form).submit();
                            var submitButton = element.find("input[type='submit']");
                            //btnText = $(submitButton).attr("value");
                            $(submitButton).attr("value", "Please Wait...");
                            $(submitButton).attr("disabled", "true");
                      //  }
                    },
                    "Cancel": function () { $(this).dialog('close'); }
                }
            });
            $(".ui-dialog-titlebar-close").hide(); // a workaround to remove the x button
            $(".ui-widget-overlay").css({ 'opacity': '0.85' });
            // Enable client side validation
            $.validator.unobtrusive.parse(this);

            // if ((this).valid())
            //     count--;
            // else
            //     count++;

            // Setup the ajax submit logic
            wireUpForm(this, updateTargetId, updateUrl);
        });

        return false;
    });
});


function wireUpForm(dialog, updateTargetId, updateUrl) {
    $('form', dialog).submit(function () {

        // Do not submit if the form
        // does not pass client side validation
        //if (!$(this).valid())
        //    count--;
        // return false;

        // Client side validation passed, submit the form
        // using the jQuery.ajax form
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                // Check whether the post was successful
                if (result.success) {
                    // Close the dialog 
                    $(dialog).dialog('close');

                    // Reload the updated data in the target div                    
                    //$(updateTargetId).load(updateUrl);
                    // Reload the updated data in the target div 
                    if (updateUrl.indexOf('javascript:') == 0) {
                        var func = updateUrl.substring(updateUrl.indexOf(':') + 1, updateUrl.indexOf('('));
                        window[func]();
                    } else {
                        $(updateTargetId).load(updateUrl);
                    }
                } else {
                    // Reload the dialog to show model errors                    
                    $(dialog).html(result);

                    // Enable client side validation
                    $.validator.unobtrusive.parse(dialog);

                    // Setup the ajax submit logic
                    wireUpForm(dialog, updateTargetId, updateUrl);
                }
            }
        });
        return false;
    });
}




(function () {
    var $t = $.telerik;
    var fx = $t.fx.slide.defaults();
    var themeRegex = /[\?&]theme=([^&#]*)/;
    var availableSkins = 'Black,Default,Forest,Hay,Metro,Office2007,Office2010Black,Office2010Blue,Office2010Silver,Outlook,Simple,Sitefinity,Sunset,Telerik,Transparent,Vista,Web20,WebBlue,Windows7'.split(',');

    function onThemeChange(e) {
        if ($(e.target).is('.simple-link'))
            return;

        e.preventDefault();

        var theme = this.className.replace('theme-preview-', '');

        var url = window.location.href;
        var newUrl = new $t.stringBuilder();

        if (url.indexOf('theme=') > 0) {
            var matches = themeRegex.exec(url);
            var oldThemeIndex = url.indexOf('theme=') + 6;

            newUrl
                .cat(url.substring(0, oldThemeIndex))
                .cat(theme)
                .cat(url.substring(oldThemeIndex + matches[1].length));
        } else {
            // won't work with hashes in url
            newUrl
                .cat(url)
                .cat((url.indexOf('?') > 0) ? '&' : '?')
                .cat('theme=')
                .cat(theme);
        }

        window.location.href = newUrl.string();
    }

    function getThemeGalleryHtml() {
        var themeGalleryHtml = new $t.stringBuilder();

        themeGalleryHtml.cat('<div id="theme-gallery"><h2>Choose Skin</h2><ul>');

        var url = window.location.href;
        var currentTheme = themeRegex.test(url) ? themeRegex.exec(url)[1] : 'vista';

        $.each(availableSkins, function () {
            themeGalleryHtml
                .cat('<li')
                .cat((this.toLowerCase() == currentTheme) ? ' class="selected"' : '')
                .cat('><a href="#" class="theme-preview-')
                .cat(this.toLowerCase())
                .cat('"><img src="')
                .cat(themePreviewsLocation).cat('/').cat(currentComponent).cat('/')
                .cat(this)
                .cat('.png" alt="" width="90" height="90" />')
                .cat('<span>')
                .cat(this)
                .cat('</span>')
                .cat('</a></li>');
        });

        themeGalleryHtml.cat('</ul>')
        //.catIf('<h2>Want more?</h2><a class="simple-link" href="http://stylebuilder.telerik.com/New.aspx?Suite=aspnet-mvc">&raquo; Create your own with the Visual Style Builder</a>', currentComponent.toLowerCase() != "chart")
            .cat('</div>');

        return themeGalleryHtml.string();
    }

    var themeGalleryOpened = false;

    $('#theming .t-drop-down')
        .click(function (e) {
            e.preventDefault();

            if (!themeGalleryOpened)
                e.stopPropagation();

            var $themeGallery = $('#theme-gallery');

            if ($themeGallery.length == 0) {
                $themeGallery = $(getThemeGalleryHtml()).appendTo(this.parentNode)

                $themeGallery.find('a').click(onThemeChange);

                $(document).click(function (e, element) {
                    if (e.which == 3)
                        return;

                    if ($(e.target).parents('#theme-gallery').length > 0)
                        return;

                    $('#theming .t-drop-down').removeClass('state-active');
                    $t.fx.rewind(fx, $themeGallery, { direction: 'bottom' });
                    themeGalleryOpened = false;
                });
            }

            $(this).addClass('state-active');
            $t.fx.play(fx, $themeGallery, { direction: 'bottom' });
            themeGalleryOpened = true;
        });
});


// Number formatting helper
function formatNumber(nStr)
{
	nStr += '';
	x = nStr.split('.');
	x1 = x[0];
	x2 = x.length > 1 ? '.' + x[1] : '';
	var rgx = /(\d+)(\d{3})/;
	while (rgx.test(x1)) {
		x1 = x1.replace(rgx, '$1' + ',' + '$2');
	}
	return x1 + x2;
}