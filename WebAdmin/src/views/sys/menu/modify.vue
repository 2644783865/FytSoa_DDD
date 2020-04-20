<template>
	<el-dialog :title="dialog.title" :visible.sync="dialogVisible" @close="onClose" width="50%">
		<el-row :gutter="15">
			<el-form ref="elForm" :model="formData" :rules="rules" size="medium" label-width="100px">
				<el-col :span="24">
					<el-form-item label="所属上级" prop="parent">
						<el-cascader
							v-model="formData.parent"
							:options="parentOptions"
							:props="parentProps"
							:style="{width: '100%'}"
							placeholder="请选择所属上级"
							clearable
						></el-cascader>
					</el-form-item>
				</el-col>
				<el-col :span="12">
					<el-form-item label="菜单类型" prop="types">
						<el-radio-group v-model="formData.types" size="medium">
							<el-radio
								v-for="(item, index) in typesOptions"
								:key="index"
								:label="item.value"
								:disabled="item.disabled"
							>{{item.label}}</el-radio>
						</el-radio-group>
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
					<el-form-item label="菜单图标" prop="icon">
						<el-select v-model="formData.icon" placeholder="请选择菜单图标" clearable :style="{width: '100%'}">
							<el-option
								v-for="(item, index) in iconOptions"
								:key="index"
								:label="item.label"
								:value="item.value"
								:disabled="item.disabled"
							></el-option>
						</el-select>
					</el-form-item>
				</el-col>
				<el-col :span="12">
					<el-form-item label="菜单名称" prop="name">
						<el-input
							v-model="formData.name"
							placeholder="请输入菜单名称"
							:maxlength="30"
							show-word-limit
							clearable
							:style="{width: '100%'}"
						></el-input>
					</el-form-item>
				</el-col>
				<el-col :span="12">
					<el-form-item label="权限标识" prop="code">
						<el-input
							v-model="formData.code"
							placeholder="请输入权限标识"
							:maxlength="30"
							show-word-limit
							clearable
							:style="{width: '100%'}"
						></el-input>
					</el-form-item>
				</el-col>
				<el-col :span="24">
					<el-form-item label="路由地址" prop="urls">
						<el-input
							v-model="formData.urls"
							placeholder="请输入路由地址"
							:maxlength="100"
							show-word-limit
							clearable
							suffix-icon="el-icon-link"
							:style="{width: '100%'}"
						></el-input>
					</el-form-item>
				</el-col>
				<el-col :span="24">
					<el-form-item label="排序" prop="sort" required>
						<el-slider :max="100" :step="1" v-model="formData.sort"></el-slider>
					</el-form-item>
				</el-col>
				<el-col :span="24">
					<el-form-item label="按钮权限" prop="btnFun">
						<el-checkbox-group v-model="formData.btnFun" size="medium">
							<el-checkbox
								v-for="(item, index) in btnFunOptions"
								:key="index"
								:label="item.value"
								:disabled="item.disabled"
							>{{item.label}}</el-checkbox>
						</el-checkbox-group>
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
import _ from 'lodash'
export default {
	data() {
		return {
			dialog: {
				title: '添加菜单'
			},
			formData: {
				id: undefined,
				parent: [],
				types: 1,
				status: true,
				icon: undefined,
				name: undefined,
				code: undefined,
				urls: undefined,
				sort: 1,
				btnFun: []
			},
			rules: {
				parent: [],
				types: [
					{
						required: true,
						message: '菜单类型不能为空',
						trigger: 'change'
					}
				],
				status: [
					{
						required: true,
						message: '状态不能为空',
						trigger: 'change'
					}
				],
				icon: [
					{
						required: true,
						message: '请选择菜单图标',
						trigger: 'change'
					}
				],
				name: [
					{
						required: true,
						message: '请输入菜单名称',
						trigger: 'blur'
					}
				],
				code: [
					{
						required: true,
						message: '请输入权限标识',
						trigger: 'blur'
					}
				],
				urls: [
					{
						required: true,
						message: '请输入路由地址',
						trigger: 'blur'
					}
				],
				btnFun: [
					{
						required: true,
						type: 'array',
						message: '请至少选择一个按钮权限',
						trigger: 'change'
					}
				]
			},
			parentOptions: [],
			typesOptions: [
				{
					label: '目录',
					value: 1
				},
				{
					label: '菜单',
					value: 2
				}
			],
			statusOptions: [
				{
					label: '显示',
					value: true
				},
				{
					label: '隐藏',
					value: false
				}
			],
			iconOptions: [
				{
					label: '选项一',
					value: 1
				},
				{
					label: '选项二',
					value: 2
				}
			],
			btnFunOptions: [
				{
					label: '选项一',
					value: 1
				},
				{
					label: '选项二',
					value: 2
				}
			],
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
				id: undefined,
				parent: [],
				types: undefined,
				status: undefined,
				icon: undefined,
				name: undefined,
				code: undefined,
				urls: undefined,
				sort: 0,
				btnFun: []
			}
			this.$refs['elForm'].resetFields()
		},
		close() {
			this.dialogVisible = false
			this.$emit('update:visible', false)
		},
		refreshSelect() {
			var _this = this
			this.$api.sys.menu
				.getSelect()
				.then(({ statusCode, data, message }) => {
					if (statusCode == 200) {
						this.parentOptions = data
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
			this.formData.id = '0'
			this.dialog.title = '添加菜单'
			this.dialogVisible = true
		},
		handelModify(record) {
			record.parent = []
			var str = record.parentGroupId.split(',')
			str.forEach(function(item, i) {
				if (item != record.id) {
					record.parent.push(item)
				}
			})
			this.refreshSelect()
			this.dialog.title = '编辑菜单'
			this.dialogVisible = true
			this.$nextTick(() => {
				this.formData = _.cloneDeep(record)
			})
		},
		handelConfirm() {
			this.$refs['elForm'].validate(valid => {
				if (!valid) return
				if (this.formData.id == '0') {
					this.formData.id = ''
					this.$api.sys.menu
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
					this.$api.sys.menu
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