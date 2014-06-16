function get_attribute_value(obj, attrib, def) {
    if (obj[attrib]) {
        return obj[attrib];
    }
    return def;
}