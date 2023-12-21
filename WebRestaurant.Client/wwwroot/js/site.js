// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $('.add-to-cart-button').click(function () {
        var productId = $(this).data('product-id'); // Получаем productId из атрибута data-product-id
        var quantity = $(this).siblings('.quantity-input').val(); // Получаем выбранное количество товара

        if (quantity <= 0) {
            alert('Количество блюд должно быть положительным числом.');
            return; // Прерываем выполнение запроса
        }

        if (quantity > 10) {
            alert('Выбрано слшиком большое количество одного блюда');
            return; // Прерываем выполнение запроса
        }

        // Отправляем асинхронный запрос на сервер
        $.ajax({
            url: '/Home/AddToCart',  // Путь к обработчику на сервере
            type: 'POST',             // HTTP-метод запроса
            data:
            {
                productId: productId,
                quantity: quantity
            }, // Передаем выбранный productId на сервер
            success: function (response) {
                // Обработка успешного ответа от сервера
                alert('Товар добавлен в корзину!');
            },
            error: function (xhr, status, error) {
                // Обработка ошибки
                alert('Произошла ошибка при добавлении товара в корзину.');
            }
        });
    });
});

let currentRating = 0;

function openRatingModal() {
    const modal = document.getElementById('ratingModal');
    modal.style.display = 'block';
}

function closeRatingModal() {
    const modal = document.getElementById('ratingModal');
    modal.style.display = 'none';
}

function setRating(rating) {
    currentRating = rating;
}

function submitRating() {
    updateAverageRating();
    closeRatingModal();
}

function updateAverageRating() {
    const averageElement = document.getElementById('average');
    averageElement.textContent = currentRating;
    // Здесь также можно отправить текущий рейтинг на сервер для сохранения
}