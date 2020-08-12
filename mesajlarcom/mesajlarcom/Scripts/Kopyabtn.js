function copy(copyId) {
    var $inp = $("<input>");
    $("body").append($inp);
    $inp.val($(copyId).
        text()).select();
    document.execCommand("copy");
    $inp.remove();
    $(".alert").fadeIn(500,
        function () {
            $(".alert").fadeOut();
        });
}
$(document).ready(function () {
    $(".copyButton1").click(
        function () {
            var $inp = $("<input>");
            $("body").append($inp);
            $inp.val($(this).parent().find(".card-text").text()).select();
            document.execCommand("copy");
            $inp.remove();
        });
});
