import request from '@/api/request'
export default {
    getList: (params) => request.get('sysorganize', params),
    getSelect: (params) => request.get('sysorganize/select', params),
    submit: (params) => request.post('sysorganize', params),
    delete: (params) => request.delete('sysorganize', params),
    modify: (params) => request.put('sysorganize', params)
}