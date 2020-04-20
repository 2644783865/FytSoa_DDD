<template>
	<div>
		<el-dialog
			:title="dialog.title"
			:visible.sync="dialogVisible"
			v-bind="$attrs"
			v-on="$listeners"
			@close="onClose"
			width="45%"
		>
			<el-row :gutter="15">
				<el-form ref="elForm" :model="formData" :rules="rules" size="medium" label-width="100px">
					<el-col :span="24">
						<el-form-item label="角色组" prop="parentId">
							<el-select
								v-model="formData.parentId"
								placeholder="请选择角色组"
								clearable
								:style="{width: '100%'}"
							>
								<el-option
									v-for="(item, index) in parentIdOptions"
									:key="index"
									:label="item.label"
									:value="item.value"
									:disabled="item.disabled"
								></el-option>
							</el-select>
						</el-form-item>
					</el-col>
					<el-col :span="24">
						<el-form-item label="角色名称" prop="name">
							<el-input
								v-model="formData.name"
								placeholder="请输入角色名称"
								:maxlength="30"
								show-word-limit
								clearable
								:style="{width: '100%'}"
							></el-input>
						</el-form-item>
					</el-col>
					<el-col :span="12">
						<el-form-item label="是否超管" prop="isSystem" required>
							<el-switch v-model="formData.isSystem" active-text="如果是超管，则不允许删除"></el-switch>
						</el-form-item>
					</el-col>
					<el-col :span="12">
						<el-form-item label="状态" prop="status">
							<el-radio-group v-model="formData.status" size="medium">
								<el-radio
									v-for="(item, index) in statusOptions"
									:key="index"
									:label="item.value"
									:disabled="item.disabled"
								>{{item.label}}</el-radio>
							</el-radio-group>
						</el-form-item>
					</el-col>
					<el-col :span="24">
						<el-form-item label="滑块" prop="sort" required>
							<el-slider :max="100" :step="1" v-model="formData.sort"></el-slider>
						</el-form-item>
					</el-col>
					<el-col :span="24">
						<el-form-item label="多行文本" prop="summary">
							<el-input
								v-model="formData.summary"
								type="textarea"
								placeholder="请输入多行文本"
								:maxlength="500"
								show-word-limit
								:autosize="{minRows: 2, maxRows: 4}"
								:style="{width: '100%'}"
							></el-input>
						</el-form-item>
					</el-col>
				</el-form>
			</el-row>
			<div slot="footer">
				<el-button @click="close">取消</el-button>
				<el-button type="primary" @click="handelConfirm">确定</el-button>
			</div>
		</el-dialog>
	</div>
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
				title: '添加岗位'
			},
			formData: {
				id: undefined,
				parentId: undefined,
				name: undefined,
				isSystem: false,
				status: true,
				sort: 1,
				summary: undefined
			},
			rules: {
				parentId: [
					{
						required: true,
						message: '请选择角色组',
						trigger: 'change'
					}
				],
				name: [
					{
						required: true,
						message: '请输入角色名称',
						trigger: 'blur'
					}
				],
				status: [
					{
						required: true,
						message: '状态不能为空',
						trigger: 'change'
					}
				],
				summary: []
			},
			parentIdOptions: [],
			statusOptions: [
				{
					label: '正常',
					value: true
				},
				{
					label: '停用',
					value: false
				}
			],
			dialogVisible: false,
			btnLoading: false
		}
	},
	computed: {},
	watch: {},
	created() {},
	mounted() {},
	methods: {
		onClose() {
			this.$refs['elForm'].resetFields()
		},
		close() {
			this.dialogVisible = false
			this.$emit('update:visible', false)
		},
		refreshSelect() {
			var _this = this
			this.$api.sys.role
				.getGroup()
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
			this.refreshSelect()
			this.formData.id = undefined
			this.dialog.title = '添加岗位'
			this.dialogVisible = true
		},
		handelModify(record) {
			this.dialog.title = '编辑岗位'
			this.dialogVisible = true
			this.refreshSelect()
			this.$nextTick(() => {
				this.formData = _.cloneDeep(record)
			})
		},
		handelConfirm() {
			this.$refs['elForm'].validate(valid => {
				if (!valid) return
				if (!this.formData.id) {
					this.$api.sys.role
						.submit(this.formData)
						.then(({ statusCode, data, message }) => {
							if (statusCode == 200) {
								this.close()
								this.$emit('complete')
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
					console.log(this.formData)
					this.$api.sys.role
						.modify(this.formData)
						.then(({ statusCode, data, message }) => {
							if (statusCode == 200) {
								this.close()
								this.$emit('complete')
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
