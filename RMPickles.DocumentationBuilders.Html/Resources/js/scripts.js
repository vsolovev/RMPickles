function initializeToc() {
    $(".tocCollapser").one("click", function() {
        collapseToc();
    });
}

function collapseToc() {
    /* set class of toc element to collapsed. CSS will do the rest*/
    $("#toc").addClass("collapsed");

    /* change the text and title of the collapser to make it appear as an expander */
    var tocCollapser = $(".tocCollapser");
    tocCollapser.text("»");
    tocCollapser.attr("title", "Expand Table of Content");

    /* register a one-time handler for the click event that will expand the toc. */
    $(".tocCollapser").one("click", function() {
        expandToc();
    });
}

function expandToc() {
    /* removes the collapsed class of toc element. CSS will do the rest*/
    $("#toc").removeClass("collapsed");

    /* change the text and title of the collapser to make it appear as an collapser again */
    var tocCollapser = $(".tocCollapser");
    tocCollapser.text("«");
    tocCollapser.attr("title", "Collapse Table of Content");

    /* register a one-time handler for the click event that will collapse the toc. */
    $(".tocCollapser").one("click", function() {
        collapseToc();
    });
}

function showImageLink(scenarioSlug) {
  var url = window.location.origin + window.location.pathname + window.location.search;

  if (scenarioSlug != null && scenarioSlug != '') {
    url = url + '#' + scenarioSlug;
  }

  window.prompt("Scenario Link: (Ctrl+C to copy)", url);
}


function copyFileContent(elementId) {

    var copyText = document.getElementById(elementId);
    var children = [].slice.call(copyText.childNodes);
    var textArea = document.createElement("textarea");

    for (var i in children) {
        if (children[i].nodeName == "BR") textArea.textContent += '\r\n';
        if (children[i].nodeName == "#text") textArea.textContent += children[i].textContent;
    }

    document.body.appendChild(textArea);
    textArea.select();
    document.execCommand("Copy");
    textArea.remove();
}

function download(filename, elementId) {


    var copyText = document.getElementById(elementId);
    var children = [].slice.call(copyText.childNodes);
    var textArea = document.createElement("textarea");

    for (var i in children) {
        if (children[i].nodeName == "BR") textArea.textContent += '\r\n';
        if (children[i].nodeName == "#text") textArea.textContent += children[i].textContent;
    }

    document.body.appendChild(textArea);

    var element = document.createElement('a');
    element.setAttribute('href', 'data:text/plain;charset=utf-8,' + encodeURIComponent(textArea.textContent));
    element.setAttribute('download', filename);

    element.style.display = 'none';
    document.body.appendChild(element);

    element.click();
    textArea.remove();
    element.remove();
}

function brToNewLine(str) {
    return str.replace(/<br ?\/?>/g, "\n");
}
