//Validate the data
function checkLength(id, value, required, length) {
    if (value.length > length) {
        $(`#${id}-error`).html(`The ${id} is too long! Please enter a ${id} that is less than ${length} characters.`);
        return 1;
    } else if (value.length < 1 && required) {
        $(`#${id}-error`).html(`The ${id} is required!`);
        return 1;
    } else {
        $(`#${id}-error`).html("");
        return 0;
    }
}