import axios from 'axios'
import qs from 'qs'
import { Notification, MessageBox, Message } from 'element-ui'

const service = axios.create({
    // axios中请求配置有baseURL选项，表示请求URL公共部分
    baseURL: 'http://192.168.120.67:5000/',
    // 超时
    timeout: 10000,
    withCredentials: false,
    headers: { 'content-type': 'application/json' }
});

//请求拦截器
// service.interceptors.request.use(
//     config => {
//         if (getToken()) {
//             config.headers['Authorization'] = 'Bearer ' + getToken() // 让每个请求携带自定义token 请根据实际情况自行修改
//         }
//         return configcle
//     },
//     error => {
//         console.log(error)
//         Promise.reject(error)
//     }
// )

//响应拦截器
// service.interceptors.response.use(res => {
//         const code = res.data.code
//         if (code === 401) {
//             MessageBox.confirm(
//                 '登录状态已过期，您可以继续留在该页面，或者重新登录',
//                 '系统提示', {
//                     confirmButtonText: '重新登录',
//                     cancelButtonText: '取消',
//                     type: 'warning'
//                 }
//             ).then(() => {
//                 store.dispatch('LogOut').then(() => {
//                     location.reload() // 为了重新实例化vue-router对象 避免bug
//                 })
//             })
//         } else if (code !== 200) {
//             Notification.error({
//                 title: res.data.msg
//             })
//             return Promise.reject('error')
//         } else {
//             return res.data
//         }
//     },
//     error => {
//         console.log('err' + error)
//         Message({
//             message: error.message,
//             type: 'error',
//             duration: 5 * 1000
//         })
//         return Promise.reject(error)
//     }
// )

const request = {
    /**
     * Post
     * @param url
     * @param data
     * @returns {Promise<unknown>}
     */
    post(url, data) {
        return new Promise((resolve, reject) => {
            service.post(url, JSON.stringify(data))
                .then(res => {
                    resolve(res.data)
                }, err => {
                    reject(err)
                }).catch(err => {
                    reject(err)
                })
        })
    },
    /**
     * Get
     * @param url
     * @param params
     * @returns {Promise<unknown>}
     */
    get(url, params) {
        return new Promise((resolve, reject) => {
            service.get(url, { params: params })
                .then(res => {
                    resolve(res.data)
                }, err => {
                    reject(err)
                }).catch(err => {
                    reject(err)
                })
        })
    },
    /**
     * Delete
     * @param url
     * @param params
     * @returns {Promise<unknown>}
     */
    delete(url, params) {
        return new Promise((resolve, reject) => {
            service.delete(url, { data: params })
                .then(res => {
                    resolve(res.data)
                }, err => {
                    reject(err)
                }).catch(err => {
                    reject(err)
                })
        })
    },
    /**
     * Put
     * @param url
     * @param params
     * @returns {Promise<unknown>}
     */
    put(url, data) {
        return new Promise((resolve, reject) => {
            service.put(url, JSON.stringify(data))
                .then(res => {
                    resolve(res.data)
                }, err => {
                    reject(err)
                }).catch(err => {
                    reject(err)
                })
        })
    },
    /**
     * Upload
     * @param url
     * @param formData
     * @param config
     * @returns {Promise<unknown>}
     */
    upload(url, formData, config) {
        return new Promise((resolve, reject) => {
            service({
                method: 'post',
                url: url,
                data: formData,
                headers: {
                    'content-type': 'multipart/form-data'
                },
                ...config
            }).then(res => {
                resolve(res.data)
            }, err => {
                reject(err)
            }).catch(err => {
                reject(err)
            })
        })
    }
}

export default request