$(document).ready(function () {
    // executes when HTML-Document is loaded and DOM is ready

    var buttons = document.querySelectorAll(".glyphicon-star");
    buttons.forEach(function (cur) {
        cur.onclick = function () {
            var req = new XMLHttpRequest();
            req.open("POST", "api/GalleryEntry_ApplicationUserApi/");
            req.setRequestHeader("Content-Type", "application/json");
            req.onload = function () { console.log("lol"); location.reload(); };
            req.onerror = function () { console.log("naw"); location.reload(); };
            var json = { "GalleryEntryID": parseInt(this.id), "ApplicationUserId": 1 };
            req.send(JSON.stringify(json));
        }
    });

    // fetch('/api/TagsApi')
    //     .then(function (response) {
    //         return response.json()
    //     }).then(function (json) {
    //         let form = $('#tag-form');
    //         json.reverse();
    //         json.forEach(tag => {
    //             let div = $('<div />');
    //             div.addClass('checkbox');

    //             let label = $('<label />');

    //             let input = $('<input />');
    //             input.attr('type', 'checkbox');
    //             input.attr('name', 'tags');
    //             input.attr('value', tag.tagID);

    //             label.append(input);
    //             label.append(tag.name);
    //             div.append(label);
    //             form.prepend(div);

    //         });

    //     }).catch(function (ex) {
    //         console.log('parsing failed', ex)
    //     });


});


