$(document).ready(() => {
    const sortBy = (opt) => {
        const userName = $('#userName').val();
        const visitsCheckbox = $('#visitsCheckbox').is(':checked');
        const emailCheckbox = $('#emailCheckbox').is(':checked');
        const phoneCheckbox = $('#phoneCheckbox').is(':checked');
        const token = $('input[name="__RequestVerificationToken"]').val();

        $.ajax({
            url: filterUsersPath,
            type: 'GET',
            data: {
                userName,
                visitsCheckbox,
                emailCheckbox,
                phoneCheckbox,
                sortBy: opt
            },
            headers: {
                RequestVerificationToken: token
            },
            success: renderUserList,
            error: (xhr) => alert('An error occurred while fetching the users.')
        });
    };

    const createUserRow = (user) => {
        const detailUrl = `${detailsPath}/${user.Id}`;
        const deleteUrl = `${deletePath}/${user.Id}`;

        return `<tr>
            <td>${user.UserName}</td>
            <td>${user.Email}</td>
            <td>${user.PhoneNumber}</td>
            <td>${user.FirstName}</td>
            <td>${user.LastName}</td>
            <td>${user.Logins.length}</td>
            <td>
                <a href="${detailUrl}" class="btn btn-primary">Detail</a>
                <a href="${deleteUrl}" class="btn btn-danger">Delete</a>
            </td>
        </tr>`;
    };

    const renderUserList = (users) => {
        const tableBody = $('#user-table-body');
        tableBody.empty();

        if (users.length === 0) {
            $.ajax({
                url: '/Error/_404',
                type: 'GET',
                success: (data) => tableBody.html(data),
                error: () => alert('An error occurred while loading the error page.')
            });
        } else {
            users.forEach((user) => tableBody.append(createUserRow(user)));
        }
    };

    const assignClickHandler = (id, sortType) => {
        $(`#${id}`).click(() => sortBy(sortType));
    };

    assignClickHandler('loginSort', 'loginSort');
    assignClickHandler('usernameSort', 'usernameSort');
    assignClickHandler('nameSort', 'nameSort');
    assignClickHandler('searchButton');
});