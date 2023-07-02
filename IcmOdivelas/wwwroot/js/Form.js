document.addEventListener("DOMContentLoaded", function () {
    var inputs = document.querySelectorAll("[id^='validationServer']");

    for (var i = 0; i < inputs.length; i++) {
        var input = inputs[i];
        var inputId = input.getAttribute("id");
        var feedbackId = "feedback-" + inputId;

        input.addEventListener("focus", function () {
            var feedbackId = "feedback-" + this.getAttribute("id");

            if (this.value.trim() === "") {
                this.classList.add("is-invalid");
                document.getElementById(feedbackId).innerText = "Campo obrigatório.";
            }
        });

        input.addEventListener("input", function () {
            var feedbackId = "feedback-" + this.getAttribute("id");

            if (this.value.trim() !== "") {
                this.classList.remove("is-invalid");
                this.classList.add("is-valid");
                document.getElementById(feedbackId).innerText = "";
            } else {
                this.classList.remove("is-valid");
                this.classList.add("is-invalid");
                document.getElementById(feedbackId).innerText = "Campo obrigatório.";
            }
        });
    }
});
