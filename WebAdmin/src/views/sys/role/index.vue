<template>
	<p>{{msg}}</p>
</template>

<script>
export default {
	data() {
		return {
			msg: 'role'
		}
	},
	mounted() {
        this.$api.sys.role.getList({
                    page: 1
                }).then(({statusCode, data}) => {
                   console.log(statusCode);
                   console.log(data);
                }).catch(() => {
                    this.loading = false
                })
	},
	methods: {}
}
</script>