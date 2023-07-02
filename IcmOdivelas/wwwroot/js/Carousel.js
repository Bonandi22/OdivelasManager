// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    // Define o intervalo de tempo entre os slides em milissegundos
    var interval = 6000;

    // Função para avançar para o próximo slide
    function carouselNext() {
        $('.carousel').carousel('next');
    }

    // Inicia a navegação automática do carrossel
    setInterval(carouselNext, interval);
});

