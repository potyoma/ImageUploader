const apiUrl = window.location.href + "api/uploads";
const apiGetUrl = "api/get/";
const progressBar = document.getElementById("progress-bar-screen")
const uploadedImageScreen = document.getElementById("uploaded-image");
const mainScreen = document.getElementById("main-card");
const uploadedImageShow = document.getElementsByClassName("uploaded-image-show")[0];
const progressBarFill = document.querySelector(
    "#progressBar > .progress-bar-fill");
const progressBarText = progressBarFill.querySelector(".progress-bar-text");
const mimeTypes = ["image/jpeg", "image/gif", "image/png", "image/svg+xml"];
const linkForDownloading = document.getElementById("linkForDownloading");

function transferWithAjax(url, formData, fileName) {
    $.ajax({
        url: url,
        data: formData,
        processData: false,
        contentType: false,
        type: "POST",
        xhr: function () {
            // Upload progress.
            var xhr = $.ajaxSettings.xhr();
            xhr.upload.addEventListener("progress", function (event) {
                let percent;

                if (event.lengthComputable) {
                    percent = (event.loaded / event.total) * 100;

                    progressBarFill.style.width = percent.toFixed(2) + "%";
                }
                percent = 0;
            }, false);

            return xhr;
        },
        success: function (data) {
            progressBar.style.display = "none";
            uploadedImageScreen.style.display = "block";
            uploadedImageShow.style.backgroundImage = "url('../Files/" + fileName + "')"
        }
    });
}

function uploadFiles(inputId) {
    let input = document.getElementById(inputId);
    let file = input.files[0];
    let formData = new FormData();

    writeLinkToField(file.name);

    formData.append("uploadedFile", file);
    mainScreen.style.display = "none";
    progressBar.style.display = "block";

    transferWithAjax(apiUrl, formData, file.name);
}

function initializeDragAndDropArea() {
    if (typeof (window["FileReader"]) == "undefined") {
        return;
    }

    let dragAndDropArea = $("#dragAndDropArea");

    if (dragAndDropArea.length == 0) {
        return;
    }

    dragAndDropArea[0].ondragover = function () {
        dragAndDropArea.addClass("drag-and-drop-area-dragging");
        return false;
    }

    dragAndDropArea[0].ondragleave = function () {
        dragAndDropArea.removeClass("drag-and-drop-area-dragging");
        return false;
    }

    dragAndDropArea[0].ondrop = function (event) {
        event.preventDefault();

        dragAndDropArea.removeClass("drag-and-drop-dragging");

        if (!mimeTypes.includes(event.dataTransfer.files[0].type)) {
            return;
        }

        var formData = new FormData();

        let file = event.dataTransfer.files[0];

        formData.append("uploadedFile", file);

        mainScreen.style.display = "none";
        progressBar.style.display = "block";

        writeLinkToField(file.name);
        transferWithAjax(apiUrl, formData, file.name);
    }
}

function writeLinkToField(fileName) {
    let selfUrl = window.location.href;
    linkForDownloading.value = selfUrl + apiGetUrl + fileName;
}

function copyToClipboard() {
    var copyText = document.getElementById("linkForDownloading");

    copyText.select();
    copyText.setSelectionRange(0, 99999);

    document.execCommand("copy");

    //alert("Copied the text: " + copyText.value);
}

function goHome() {
    progressBar.style.display = "none";
    uploadedImageScreen.style.display = "none";
    mainScreen.style.display = "block";
}

$(document).ready(
    function () {
        initializeDragAndDropArea();
    });