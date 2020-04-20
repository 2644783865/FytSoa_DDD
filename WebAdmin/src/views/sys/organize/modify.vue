<template>
	<el-dialog :title="dialog.title" :visible.sync="dialogVisible" @close="onClose" width="850px">
		<el-row :gutter="15">
			<el-form ref="elForm" :model="formData" :rules="rules" label-width="100px">
				<el-col :span="24">
					<el-form-item label="所属部门" prop="parent">
						<el-cascader
							v-model="formData.parent"
							:options="parentIdOptions"
							:props="parentIdProps"
							:style="{width: '100%'}"
							placeholder="请选择所属部门"
							clearable
							filterable
						></el-cascader>
					</el-form-item>
				</el-col>
				<el-col :span="12">
					<el-form-item label="部门名称" prop="name">
						<el-input v-model="formData.name" placeholder="请输入部门名称" clearable></el-input>
					</el-form-item>
				</el-col>
				<el-col :span="12">
					<el-form-item label="部门负责人" prop="leaderUser">
						<el-input v-model="formData.leaderUser" placeholder="请输入部门负责人" clearable></el-input>
					</el-form-item>
				</el-col>
				<el-col :span="12">
					<el-form-item label="联系电话" prop="leaderMobile">
						<el-input v-model="formData.leaderMobile" placeholder="请输入联系电话" clearable></el-input>
					</el-form-item>
				</el-col>
				<el-col :span="12">
					<el-form-item label="联系邮箱" prop="leaderEmail">
						<el-input v-model="formData.leaderEmail" placeholder="请输入联系邮箱" clearable></el-input>
					</el-form-item>
				</el-col>
				<el-col :span="12">
					<el-form-item label="状态" prop="status" required>
						<el-switch v-model="formData.status"></el-switch>
					</el-form-item>
				</el-col>
				<el-col :span="12">
					<el-form-item label="排序" prop="sort" required>
						<el-slider :max="100" :step="1" v-model="formData.sort"></el-slider>
					</el-form-item>
				</el-col>
			</el-form>
		</el-row>
		<span slot="footer" class="dialog-footer">
			<el-button @click="close">取 消</el-button>
			<el-button type="primary" @click="handelConfirm" :loading="btnLoading">确 定</el-button>
		</span>
	</el-dialog>
</template>
<script>
import os from '@/common/js/util'
export default {
	data() {
		return {
			dialog: {
				title: '添加部门'
			},
			formData: {
				id: '0',
				parent: [],
				name: '',
				leaderUser: '',
				leaderMobile: '',
				leaderEmail: '',
				status: true,
				sort: 1
			},
			rules: {
				parent: [
					{
						required: true,
						type: 'array',
						message: '请至少选择一个所属部门',
						trigger: 'change'
					}
				],
				name: [
					{
						required: true,
						message: '请输入部门名称',
						trigger: 'blur'
					}
				],
				leaderUser: [
					{
						required: true,
						message: '请输入部门负责人',
						trigger: 'blur'
					}
				],
				leaderMobile: [
					{
						required: true,
						message: '请输入联系电话',
						trigger: 'blur'
					}
				],
				leaderEmail: []
			},
			parentIdOptions: [],
			parentIdProps: {
				multiple: false,
				checkStrictly: true,
				expandTrigger: 'hover'
			},
			dialogVisible: false,
			btnLoading: false
		}
	},
	methods: {
		onClose() {
			this.formData = {
				id: '0',
				parent: [],
				name: '',
				leaderUser: '',
				leaderMobile: '',
				leaderEmail: '',
				status: true,
				sort: 1
			}
			this.$refs['elForm'].resetFields()
		},
		close() {
			this.dialogVisible = false
			this.$emit('update:visible', false)
		},
		refreshSelect(type) {
			var _this = this
			this.$api.sys.organize
				.getSelect()
				.then(({ statusCode, data, message }) => {
					if (statusCode == 200) {
						this.parentIdOptions = data
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
		},
		handleAdd() {
			this.refreshSelect(1)
			this.formData.id = '0'
			this.dialog.title = '添加部门'
			this.dialogVisible = true
		},
		handelModify(record) {
			record.parent = []
			var str = record.parentIdList.split(',')
			str.forEach(function(item, i) {
				if (item != record.id) {
					record.parent.push(item)
				}
			})
			this.refreshSelect(2)
			this.dialog.title = '编辑部门'
			this.dialogVisible = true
			this.$nextTick(() => {
				this.formData = os.deepClone(record)
			})
		},
		handelConfirm() {
			this.$refs['elForm'].validate(valid => {
				if (!valid) return
				if (this.formData.id == '0') {
					this.formData.id = ''
					this.$api.sys.organize
						.submit(this.formData)
						.then(({ statusCode, data, message }) => {
							if (statusCode == 200) {
								this.close()
								this.$emit('complete')
								this.$notify({
									message: '添加成功',
									type: 'success'
								});
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
					this.$api.sys.organize
						.modify(this.formData)
						.then(({ statusCode, data, message }) => {
							if (statusCode == 200) {
								this.close()
								this.$emit('complete')
								this.$notify({
									message: '修改成功',
									type: 'success'
								});
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