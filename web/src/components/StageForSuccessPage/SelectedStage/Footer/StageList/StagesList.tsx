import {
	Box,
	// LinearProgress,
	List,
	ListItem,
	ListItemButton,
	ListItemText,
	Typography,
} from '@mui/material'
import { useAppDispatch, useAppSelector } from '../../../../../hooks/reduxHooks'
import { changeOpenedStage } from '../../../../../store/processSlice/processSlice'
import styles from '/src/styles/MainPageStyles/SelectedProcessStyles/StagesListStyles/StagesListStyle.module.scss'
import { IStage } from '../../../../../interfaces/IApi/IGetStageApi'
import { FC, useMemo } from 'react'

const StagesList: FC<{ textForSearchProcess: string }> = ({
	textForSearchProcess,
}) => {
	// const openedProcess = useAppSelector(state => state.processes.openedProcess)
	const openedStage = useAppSelector(state => state.processes.openedStage)
	const dispatch = useAppDispatch()

	// eslint-disable-next-line react-hooks/exhaustive-deps
	const stagesList: IStage[] = [
		{
			id: 0,
			title: 'string',
			createdAt: 'string',
			signedAt: 'string',
			approvedAt: 'string',
			status: 'string',
			statusValue: 0,
			user: {
				id: 0,
				email: 'string',
				longName: 'string',
				shortName: 'string',
				roles: ['string'],
			},
			holds: [
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
			canCreate: [0],
			mark: true,
			pass: true,
		},
		{
			id: 1,
			title: 'string1',
			createdAt: 'string',
			signedAt: 'string',
			approvedAt: 'string',
			status: 'string',
			statusValue: 0,
			user: {
				id: 0,
				email: 'string',
				longName: 'string',
				shortName: 'string',
				roles: ['string'],
			},
			holds: [
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
			canCreate: [0],
			mark: true,
			pass: true,
		},
	]

	const filteredProcesses: IStage[] = useMemo(() => {
		return stagesList.filter(process =>
			process.title
				.toLocaleLowerCase()
				.includes(textForSearchProcess?.toLocaleLowerCase())
		)
	}, [stagesList, textForSearchProcess])

	return (
		<Box className={`${styles.container} ${styles.stageForSuccessContainer}`}>
			{/* {isLoading && <LinearProgress />} */}
			{/* {isSuccess && stagesList && ( */}
			<>
				<List className={styles.list}>
					{filteredProcesses
						.sort((a, b) => a.statusValue - b.statusValue)
						.map((stage, index) => (
							<ListItem
								disablePadding
								key={index}
								className={
									openedStage === stage.id
										? styles.openedProcessWrap
										: styles.closedProcessWrap
								}
							>
								{stage.status === 'Согласовано' && (
									<img
										src='/completed.svg'
										loading='lazy'
										className={styles.img}
									/>
								)}
								{stage.status === 'Не начат' && (
									<img
										src='/stoppedProcess.svg'
										loading='lazy'
										className={styles.img}
									/>
								)}
								{stage.status === 'Согласовано-Блокировано' && (
									<img src='/lock.svg' loading='lazy' className={styles.img} />
								)}
								{stage.status === 'Принят на проверку' && (
									<img
										src='/arrow-circle-down.svg'
										loading='lazy'
										className={styles.img}
									/>
								)}
								{stage.status === 'Отправлен на проверку' && (
									<img
										src='/arrow-circle-right.svg'
										loading='lazy'
										className={styles.img}
									/>
								)}
								{stage.status === 'Отменен' && (
									<img
										src='/rejected.svg'
										loading='lazy'
										className={styles.img}
									/>
								)}
								{stage.status === 'Остановлен' && (
									<img
										src='/pause-circle.svg'
										loading='lazy'
										className={styles.img}
									/>
								)}

								<ListItemButton
									className={styles.openedProcess}
									onClick={() => dispatch(changeOpenedStage({ id: stage.id }))}
								>
									<ListItemText>
										<Typography
											className={
												openedStage === stage.id
													? styles.openedProcessText
													: styles.closedProcessText
											}
										>
											{stage.title}
										</Typography>
									</ListItemText>
								</ListItemButton>
							</ListItem>
						))}
				</List>
			</>
			{/* )} */}
		</Box>
	)
}

export default StagesList
