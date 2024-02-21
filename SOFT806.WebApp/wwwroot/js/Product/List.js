$(document).ready(() => {
    const sortBy = (opt) => {
        const productName = $('#productName').val();
        const categoryCheckbox = $('#categoryCheckbox').is(':checked');
        const promotedCheckbox = $('#promotedCheckbox').is(':checked');
        const token = $('input[name="__RequestVerificationToken"]').val();

        $.ajax({
            url: filterProductsPath,
            type: 'GET',
            data: {
                productName,
                categoryCheckbox,
                promotedCheckbox,
                sortBy: opt
            },
            headers: {
                RequestVerificationToken: token
            },
            success: renderCatalog,
            error: (xhr) => alert('An error occurred while fetching the products.')
        });
    };

    const createRow = (product) => {
        const detailUrl = `${productDetailPath}/${product.Id}`;
        const deleteUrl = `${productDeletePath}/${product.Id}`;

        return `<tr>
        <td>${product.Name}</td>
        <td><img src="${product.Photo}" width="100" height="100" /></td>
        <td>${product.Stock}</td>
        <td>${product.Price}</td>
        <td>${product.Category.Name}</td>
        <td>
            <a href="${detailUrl}" class="btn btn-primary">Detail</a>
            <a href="${deleteUrl}" class="btn btn-danger">Delete</a>
            <button class="btn ${product.IsPromoted ? 'btn-warning' : 'btn-success'}" onclick="setPromotion('${product.Id}', ${product.IsPromoted})">${product.IsPromoted ? 'Demote' : 'Promote'}</button>
        </td>
    </tr>`;
    };

    const renderCatalog = (products) => {
        const tableBody = $('#product-table-body');
        tableBody.empty();

        if (products.length === 0) {
            $.ajax({
                url: '/Error/_404',
                type: 'GET',
                success: (data) => tableBody.html(data),
                error: () => alert('An error occurred while loading the error page.')
            });
        } else {
            products.forEach((product) => tableBody.append(createRow(product)));
        }
    };

    window.setPromotion = (id, isPromoted) => {
        const token = $('input[name="__RequestVerificationToken"]').val();

        $.ajax({
            url: isPromoted ? productUnPromotePath : productPromotePath,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({Id: id}),
            headers: {
                RequestVerificationToken: token
            },
            success: renderCatalog,
            error: () => alert('An error occurred while setting the promotion.')
        });
    }
    const assignClickHandler = (id, sortType) => {
        $(`#${id}`).click(() => sortBy(sortType));
    };

    assignClickHandler('toHighestPriceSort', 'toHighestPriceSort');
    assignClickHandler('toLowestPriceSort', 'toLowestPriceSort');
    assignClickHandler('categorySort', 'categorySort');
    assignClickHandler('nameSort', 'nameSort');
    assignClickHandler('searchButton');
});