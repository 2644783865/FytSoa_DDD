<template>
	<el-dialog
		:title="dialog.title"
		:visible.sync="dialogGroupVisible"
		v-bind="$attrs"
		v-on="$listeners"
		@close="onClose"
		width="35%"
	>
		<el-form
			ref="elGroupForm"
			:model="formGroupData"
			:rules="rulesGroup"
			size="medium"
			label-width="100px"
		>
			<el-form-item label="角色组" prop="name">
				<el-input
					v-model="formGroupData.name"
					placeholder="请输入角色组"
					:maxlength="30"
					show-word-limit
					clearable
					:style="{width: '100%'}"
				></el-input>
			</el-form-item>
			<el-form-item label="滑块" prop="sort" required>
				<el-slider :max="100" :step="1" v-model="formGroupData.sort"></el-slider>
			</el-form-item>
		</el-form>

		<div slot="footer">
			<el-button @click="close">取消</el-button>
			<el-button type="primary" @click="handelGroupConfirm">确定</el-button>
		</div>
	</el-dialog>
</template>
<script>
import _ from 'lodash'
export default {
	inheritAttrs: false,
	components: {},
	props: [],
	data() {
		return {
			dialog: {
				title: '添加角色组'
			},
			formGroupData: {
				id: undefined,
				parentId: '0',
				name: undefined,
				isSystem: false,
				status: true,
				sort: 1,
				summary: undefined
			},
			rulesGroup: {
				name: [
					{
						required: true,
						message: '请输入角色组',
						trigger: 'blur'
					}
				]
			},
			dialogGroupVisible: false,
			btnLoading: false
		}
	},
	computed: {},
	watch: {},
	created() {},
	mounted() {},
	methods: {
		onClose() {
			this.$refs['elGroupForm'].resetFields()
		},
		close() {
			this.dialogGroupVisible = false
			this.$emit('update:visible', false)
		},
		handleAddGroup() {
			this.formGroupData.id = undefined
			this.dialog.title = '添加角色组'
			this.dialogGroupVisible = true
		},
		handelModifyGroup(record) {
			this.dialog.title = '编辑角色组'
			this.dialogGroupVisible = true
			this.$nextTick(() => {
				this.formGroupData = _.cloneDeep(record)
			})
		},
		handelGroupConfirm() {
			this.$refs['elGroupForm'].validate(valid => {
				if (!valid) return
				if (!this.formGroupData.id) {
					this.$api.sys.role
						.submit(this.formGroupData)
						.then(({ statusCode, data, message }) => {
							if (statusCode == 200) {
								this.close()
								this.$emit('groupcomplete')
								this.$notify({
									message: '添加成功',
									type: 'success'
								})
							} else {
								this.$notify({
									message: message,
									type: 'error'
								})
							}
						})
						.catch(() => {
							this.loading = false
						})
				} else {
					console.log(this.formGroupData)
					this.$api.sys.role
						.modify(this.formGroupData)
						.then(({ statusCode, data, message }) => {
							if (statusCode == 200) {
								this.close()
								this.$emit('groupcomplete')
								this.$notify({
									message: '修改成功',
									type: 'success'
								})
							} else {
								this.$notify({
									message: message,
									type: 'error'
								})
							}
						})
						.catch(() => {
							this.loading = false
						})
				}
			})
		}
	}
}
</script>
<style>
</style>
