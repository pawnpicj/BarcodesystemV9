let EventCopyFromPOService = {
    iEventCopyFromPO: new IEventCopyFromPO(),
    ChoosePO_Click(temporyListPO, TbCopyFromPO) {
        let response = EventCopyFromPOService.iEventCopyFromPO.ChoosePO_Click(temporyListPO, TbCopyFromPO);
        return response;
    }
}