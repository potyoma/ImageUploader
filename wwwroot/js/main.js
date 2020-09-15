// This is working.

const apiUrl = "/api/uploadscontroller";
const progressBarFill = document.querySelector(
    "#progressBar > .progress-bar-fill");
const progressBarText = progressBarFill.querySelector(".progress-bar-text");

function transferWithAjax(url, formData) {
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
                    progressBarText.textContent = percent.toFixed(2) + "%"
                }
                percent = 0;
            }, false);
            
            return xhr;
        },
        success: function (data) {
            alert("Files uploaded!");
        }
    });
}
// The rest should be changed.


function uploadFiles(inputId) {
    let input = document.getElementById(inputId);
    let file = input.files[0];
    let formData = new FormData();

    formData.append("uploadedFile", file);

    transferWithAjax(apiUrl, formData);
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
        dragAndDropArea.removeClass("drag-and-drop-dragging");

        var formData = new FormData();

        formData.append("uploadedFile", event.dataTransfer.files[0]);

        transferWithAjax(apiUrl, formData);
    }
}

$(document).ready(
    function () {
        initializeDragAndDropArea();
    });