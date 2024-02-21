$(document).ready(function () {
    $("#photoUrlInput").on("input", function () {
        var imageUrl = $(this).val();
        $("#previewImage").attr("src", imageUrl);
    });
});