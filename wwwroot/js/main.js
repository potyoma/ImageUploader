const apiUrl = "/api/uploadscontroller";

function uploadFiles(inputId) {
    let input = document.getElementById(inputId);
    let file = input.files[0];
    let formData = new FormData();

    console.log(file);

    formData.append("uploadedFile", file);

    console.log(formData);

    transferWithAjax(apiUrl, formData);
}

function initializeDragAndDropArea() {
    if (typeof(window["FileReader"]) == "undefined") {
        return;
    }

    let dragAndDropArea = $("#dragAndDropArea");

    if (dragAndDropArea.length == 0) {
        return;
    }

    dragAndDropArea[0].ondragover = function() {
        dragAndDropArea.addClass("drag-and-drop-area-dragging");
        return false;
    }

    dragAndDropArea[0].ondragleave = function() {
        dragAndDropArea.removeClass("drag-and-drop-area-dragging");
        return false;
    }

    dragAndDropArea[0].ondrop = function(event) {
        dragAndDropArea.removeClass("drag-and-drop-dragging");

        var formData = new FormData();

        formData.append("uploadedFile", event.dataTransfer.files[0]);

        transferWithAjax(apiUrl, formData);
    }
}

function transferWithAjax(url, formData) {
    $.ajax({
        url: url,
        data: formData,
        processData: false,
        contentType: false,
        type: "POST",
        success: function(data) {
            alert("Files uploaded!");
        }
    });
}

$(document).ready(
    function() {
        initializeDragAndDropArea();
});