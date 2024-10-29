$(document).ready(function () {
    $('#notificationsDropdown').on('click', function (e) {
        e.preventDefault();
        $.ajax({
            url: '@Url.Action("Index", "Notifications")',
            type: 'GET',
            success: function (data) {
                $('#notificationsContainer').html(data);
            },
            error: function (xhr, status, error) {
                console.error("Error fetching notifications:", error);
            }
        });
    });
});
