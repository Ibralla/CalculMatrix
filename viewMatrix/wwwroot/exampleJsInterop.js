// This is a JavaScript module that is loaded on demand. It can export any number of
// functions, and may import other JavaScript modules if required.

var val, id;
export function getInputvalue() {
    $(".input").keyup(function () {
        val = $(this).text();
    });
    return val;
}

export function getInputID() {
    $(".input").keyup(function () {
        id = $(this).attr("id");
    });
    return id;
}


export function updateResult(data) {
    const myArray = data.split("\n");
    let matrix = [];
    for (let i = 0; i < myArray.length - 1; i++) {
        const row = myArray[i].split(" ");
        for (let j = 0; j < myArray[i].split(" ").length - 1; j++) {
            matrix[i] = [];
        }
    }

    for (let i = 0; i < myArray.length - 1; i++) {
        const row = myArray[i].split(" ");
        for (let j = 0; j < myArray[i].split(" ").length - 1; j++) {
            matrix[i][j] = row[j];
        }
    }

    let htmlCode = "<div style=\"border-right : 2px solid; border-left : 2px solid; border-radius: 10% / 50%; display: inline-block; padding: 3px;\">";
    htmlCode += "<div>";
    for (let i = 0; i < myArray.length - 1; i++) {
        htmlCode += "<p style=\"margin-top: 0px; margin-bottom: 0px;\">";
        for (let j = 0; j < myArray[i].split(" ").length - 1; j++) {
            htmlCode += "<span class=\"input mx-1\" >" + matrix[i][j] + "</span>";
        }
        htmlCode += "</p>";
    }
    htmlCode += "</div></div>";
    $("#result").html(htmlCode);
}