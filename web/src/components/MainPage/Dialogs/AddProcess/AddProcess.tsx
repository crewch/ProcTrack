import {
	Autocomplete,
	Dialog,
	DialogContent,
	DialogTitle,
	TextField,
} from '@mui/material'
import { CustomButton } from '../../../CustomButton/CustomButton'
import { FC, useEffect, useState } from 'react'
import { Box } from '@mui/system'
import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query'
import { IDataForSend } from '../../../../interfaces/IApi/IAddProcessApi'
import { addProcessApi } from '../../../../api/addProcessApi'
import { IAutocomplete } from '../../../../interfaces/IMainPage/IDialogs/IAddProcess/IAddProcess'
import styles from '/src/styles/MainPageStyles/ContainerListProcessStyles/AddProcessDialog/AddProcessDialog.module.scss'

const AddProcess: FC<{ open: boolean; handleClose: () => void }> = ({
	open,
	handleClose,
}) => {
	const [templates, setTemplates] = useState<IAutocomplete[]>()
	const [groups, setGroups] = useState<IAutocomplete[]>()
	const [priorities, setPriorities] = useState<IAutocomplete[]>()

	const [selectedTemplate, setSelectedTemplate] = useState<
		IAutocomplete | undefined | null
	>()
	const [selectedGroup, setSelectedGroup] = useState<
		IAutocomplete | undefined | null
	>()
	const [selectedPriority, setSelectedPriority] = useState<
		IAutocomplete | undefined | null
	>()

	const [title, setTitle] = useState<string | undefined>()

	const dataForSend: IDataForSend = {
		templateId: selectedTemplate?.id,
		groupId: selectedGroup?.id,
		process: {
			id: 0,
			title: title,
			priority: selectedPriority?.label,
			type: 'string',
			createdAt: '2023-07-05T12:38:34.439Z',
			approvedAt: '2023-07-05T12:38:34.439Z',
			expectedTime: null,
			hold: [
				{
					id: 0,
					destId: 0,
					type: 'string',
					rights: ['string'],
					users: [
						{
							id: 0,
							email: 'string',
							longName: 'string',
							shortName: 'string',
							roles: ['string'],
						},
					],
					groups: [
						{
							id: 0,
							title: 'string',
							description: 'string',
							boss: {
								id: 0,
								email: 'string',
								longName: 'string',
								shortName: 'string',
								roles: ['string'],
							},
						},
					],
				},
			],
		},
	}

	const { data: dataTemplates, isSuccess: isSuccessTemplates } = useQuery({
		queryKey: ['templatesAddProcess'],
		queryFn: addProcessApi.getTemplates,
	})

	useEffect(() => {
		if (isSuccessTemplates && dataTemplates) {
			setTemplates(
				dataTemplates.map(template => {
					return { label: template.title, id: template.id }
				})
			)
		}
	}, [dataTemplates, isSuccessTemplates])

	const { data: dataGroups, isSuccess: isSuccessGroups } = useQuery({
		queryKey: ['groupsAddProcess'],
		queryFn: addProcessApi.getGroupes,
	})

	useEffect(() => {
		if (isSuccessGroups && dataGroups) {
			setGroups(
				dataGroups.map(group => {
					return { label: group.title, id: group.id }
				})
			)
		}
	}, [dataGroups, isSuccessGroups])

	const { data: dataPriorities, isSuccess: isSuccessPriorities } = useQuery({
		queryKey: ['prioritiesAddProcess'],
		queryFn: addProcessApi.getPriorities,
	})

	useEffect(() => {
		if (isSuccessPriorities && dataPriorities) {
			setPriorities(
				dataPriorities.map(priority => {
					return { label: priority, id: 0 }
				})
			)
		}
	}, [dataPriorities, isSuccessPriorities])

	const queryClient = useQueryClient()

	const mutationAddProcess = useMutation({
		mutationFn: addProcessApi.addProcess,
		onSuccess: () => {
			queryClient.invalidateQueries({ queryKey: ['allProcess'] })
			setTitle('')
			setSelectedTemplate(undefined)
			setSelectedGroup(undefined)
			setSelectedPriority(undefined)
		},
	})

	return (
		<Dialog
			className={styles.dialog}
			maxWidth={false}
			PaperProps={{
				sx: {
					width: '70%',
					height: '80%',
					borderRadius: '16px',
					display: 'flex',
					gap: 1,
				},
			}}
			open={open}
			onClose={() => {
				handleClose()
				setTitle('')
				setSelectedTemplate(undefined)
				setSelectedGroup(undefined)
				setSelectedPriority(undefined)
			}}
		>
			<DialogTitle className={styles.dialogTitle}>
				Добавление Процесса
			</DialogTitle>
			<DialogContent className={styles.dialogContent}>
				<Box className={styles.mainContainer}>
					<Box className={styles.form}>
						<TextField
							value={title || ''}
							onChange={e => setTitle(e.target.value)}
							label='Название процесса'
						/>
						<Box className={styles.autocompletes}>
							{isSuccessTemplates && templates && (
								<Autocomplete
									value={selectedTemplate || null}
									onChange={(
										_event: unknown,
										newValue: IAutocomplete | null | undefined
									) => setSelectedTemplate(newValue)}
									disablePortal
									options={templates}
									sx={{ width: 300 }}
									renderInput={params => (
										<TextField {...params} label='Шаблон' />
									)}
								/>
							)}
							{isSuccessGroups && groups && (
								<Autocomplete
									value={selectedGroup || null}
									onChange={(
										_event: unknown,
										newValue: IAutocomplete | null | undefined
									) => setSelectedGroup(newValue)}
									disablePortal
									options={groups}
									sx={{ width: 300 }}
									renderInput={params => (
										<TextField {...params} label='Подразделение' />
									)}
								/>
							)}
							{isSuccessPriorities && priorities && (
								<Autocomplete
									value={selectedPriority || null}
									onChange={(
										_event: unknown,
										newValue: IAutocomplete | null | undefined
									) => setSelectedPriority(newValue)}
									disablePortal
									options={priorities}
									sx={{ width: 300 }}
									renderInput={params => (
										<TextField {...params} label='Важность' />
									)}
								/>
							)}
						</Box>
					</Box>
					<CustomButton
						onClick={() => {
							mutationAddProcess.mutate(dataForSend)
							handleClose()
						}}
						disabled={
							!(title && selectedGroup && selectedPriority && selectedTemplate)
						}
						className={styles.btn}
						sx={{
							fontSize: {
								lg: '14px',
							},
						}}
						variant='contained'
					>
						Добавить
					</CustomButton>
				</Box>
				<Box className={styles.graph}></Box>
			</DialogContent>
		</Dialog>
	)
}

export default AddProcess
