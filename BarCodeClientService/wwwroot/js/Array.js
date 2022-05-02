
var helper = {

    // Remove and return the first occurrence
    removeOne: function(array, predicate) {
        for (var j = 0; j < array.length; j++) {
            if (predicate(array[j])) {
                return array.splice(j, 1);
            }
        }
    },
    updateOne: function(array, predicate, obj) {
        for (var j = 0; j < array.length; j++) {
            if (predicate(array[j])) {
                return array.splice(j, 1, obj);
            }
        }
    }


};