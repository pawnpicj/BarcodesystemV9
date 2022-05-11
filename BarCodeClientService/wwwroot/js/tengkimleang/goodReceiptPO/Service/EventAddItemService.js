let EventItemAdd = {
    iEventItemAdd: new IEventItemAdd(),
    Btn_ClickBatchSerail(index, id) {
        EventItemAdd.iEventItemAdd.Btn_ClickBatchSerail(index, id);
    },
    DeleteArray(arr, value) {
        let tmpArr = EventItemAdd.iEventItemAdd.DeleteSerial(arr, value);
        lsSerial = tmpArr;
        tbSerial1.clear();
        tbSerial1.rows.add(lsSerial);
        tbSerial1.search("").draw();
    },
    getItemCode(url) {
        EventItemAdd.iEventItemAdd.getItemCode(url);
    }
}