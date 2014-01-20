
function FileUpload(formData, completeFn, progressFn, errorFn) {

    var logMsg = function (msg) {

        if (typeof msg === 'string')
            console.log("[FileUpload.js] " + msg);
        else {
            console.log("[FileUpload.js] >>")
            console.log(msg);
        }
    }

    logMsg("FileUpload")
    logMsg("formData: ");
    logMsg(formData);

    $.ajax({
        url: '../UploadTarget.aspx',  //Server script to process data
        type: 'POST',
        xhr: function () {  // Custom XMLHttpRequest
            var myXhr = $.ajaxSettings.xhr();
            if (myXhr.upload) { // Check if upload property exists
                myXhr.upload.addEventListener('progress', progressHandlingFunction, false); // For handling the progress of the upload
            }
            return myXhr;
        },
        //Ajax events
        beforeSend: beforeSendHandler,
        success: completeHandler,
        error: errorHandler,
        // Form data
        data: formData,
        //Options to tell jQuery not to process data or worry about content-type.
        cache: false,
        contentType: false,
        processData: false
    });

    function beforeSendHandler(resp) {
        logMsg('beforeSend:');
        logMsg(resp);
    }
    function errorHandler(err) {
        logMsg('error:');
        logMsg(err);
        if (errorFn) errorFn(err);
    }
    function completeHandler(resp) {
        logMsg('complete:');
        logMsg(resp);
        if (completeFn) {
            logMsg("calling completeFn...")
            completeFn(resp);
        }
    }

    function progressHandlingFunction(e) {
        if (e.lengthComputable && progressFn) {
            logMsg("calling progressFn...")
            progressFn(e.loaded / e.total);
            //$('.' + progressDiv).attr({ value: e.loaded, max: e.total });
        }
    }
}