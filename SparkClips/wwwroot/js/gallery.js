var buttons = document.querySelectorAll(".glyphicon-star");
buttons.forEach(function (cur) {
    cur.onclick = function () {
        var req = new XMLHttpRequest();
        req.open("POST", "api/GalleryEntry_ApplicationUserApi/");
        req.setRequestHeader("Content-Type", "application/json");
        req.onload = function () { console.log("lol") };
        req.onerror = function () { console.log("naw") };
        var json = { "GalleryEntryID": parseInt(this.id), "ApplicationUserId": 1 };
        req.send(JSON.stringify(json));
    }
});