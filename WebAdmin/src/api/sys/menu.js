import request from '@/api/request'
export default {
    getList: (params) => request.get('sysmenu', params),
    getSelect: (params) => request.get('sysmenu/select', params),
    submit: (params) => request.post('sysmenu', params),
    delete: (params) => request.delete('sysmenu', params),
    modify: (params) => request.put('sysmenu', params)
}