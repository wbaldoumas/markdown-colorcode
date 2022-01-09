/**
 * Adapted from https://github.com/dotnet/docfx/issues/274#issuecomment-456168196
 */

var fs = require('fs');
var yaml = require('js-yaml');
var toc = yaml.load(fs.readFileSync('../site/yml/api/toc.yml'));

var root = "Markdig.ColorCode";
var namespaces = {
    [root]: {}
};

for (var i = 0; i < toc.length; i++) {
    var fullnamespace = toc[i].uid;
    var splitnamespace = [root];
    if (fullnamespace != root) {
        var subnamespace = fullnamespace.replace(root + '.', '').split('.');
        splitnamespace.push(...subnamespace);
    }

    var parent = namespaces;

    for (var j = 0; j < splitnamespace.length; j++) {
        var partialnamespace = splitnamespace[j];

        if (parent[partialnamespace] == undefined) {
            parent[partialnamespace] = {};
        }
        parent = parent[partialnamespace];
    }

    if (parent.items == undefined) {
        parent.items = toc[i].items;
    } else {
        parent.items.push(toc[i]);
    }
}

function recurse(obj, path = "") {
    var items = [];
    Object.keys(obj).forEach((e, i) => {
        if (e != "items") {
            var newPath;
            if (path == "") {
                newPath = e;
            } else {
                newPath = path + '.' + e;
            }
            var newObj = {uid: newPath, name: e, items: obj[e].items || []}
            newObj.items.push(...recurse(obj[e], newPath));
            items.push(newObj);
        }
    });
    return items;
}

var items = recurse(namespaces);

fs.writeFileSync('../site/yml/api/toc.yml', yaml.dump(items));
