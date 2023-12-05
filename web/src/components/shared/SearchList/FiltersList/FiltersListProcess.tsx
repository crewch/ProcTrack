import {
	Accordion,
	AccordionDetails,
	AccordionSummary,
	Checkbox,
	Divider,
	FormControlLabel,
	LinearProgress,
	List,
	Typography,
} from '@mui/material'
import ExpandMoreIcon from '@mui/icons-material/ExpandMore'
import { useQuery } from '@tanstack/react-query'
import FiltersCheckboxStage from './FiltersCheckbox/FiltersCheckboxProcess'
import { useAppDispatch, useAppSelector } from '@/hooks/reduxHooks'
import { filtersService } from '@/services/filters'
import {
	toggleAllFilters,
	toggleShowCompleted,
} from '@/store/filterProcessSlice/filterProcessSlice'
import styles from './FiltersList.module.scss'

const FiltersListProcess = () => {
	const {
		data: filtersProcess,
		isLoading,
		isSuccess,
	} = useQuery({
		queryKey: ['filtersProcess'],
		queryFn: filtersService.getFiltersProcess,
	})

	const dispatch = useAppDispatch()
	const selectedFilters = useAppSelector(state => state.filterProcess)
	const showCompleted = useAppSelector(
		state => state.filterProcess.showCompleted
	)

	return (
		<List component='nav' className={styles.list}>
			{isLoading && <LinearProgress />}
			{isSuccess && filtersProcess && (
				<>
					<Accordion disableGutters className={styles.accordion}>
						<AccordionSummary expandIcon={<ExpandMoreIcon />}>
							<FormControlLabel
								label={
									<Typography className={styles.typography}>
										Важность
									</Typography>
								}
								onClick={event => event.stopPropagation()}
								onFocus={event => event.stopPropagation()}
								control={
									<Checkbox
										checked={filtersProcess.priorities.every(item =>
											selectedFilters.priorities.includes(item)
										)}
										onChange={() =>
											dispatch(
												toggleAllFilters({
													settings: filtersProcess.priorities,
													type: 'priorities',
												})
											)
										}
										name='Важность'
									/>
								}
							/>
						</AccordionSummary>
						<AccordionDetails>
							<FiltersCheckboxStage
								settings={filtersProcess.priorities}
								type='priorities'
							/>
						</AccordionDetails>
					</Accordion>
					<Accordion disableGutters className={styles.accordion}>
						<AccordionSummary expandIcon={<ExpandMoreIcon />}>
							<FormControlLabel
								label={
									<Typography className={styles.typography}>Тип</Typography>
								}
								onClick={event => event.stopPropagation()}
								onFocus={event => event.stopPropagation()}
								control={
									<Checkbox
										checked={filtersProcess.types.every(item =>
											selectedFilters.types.includes(item)
										)}
										onChange={() =>
											dispatch(
												toggleAllFilters({
													settings: filtersProcess.types,
													type: 'types',
												})
											)
										}
										name='Тип'
									/>
								}
							/>
						</AccordionSummary>
						<AccordionDetails>
							<FiltersCheckboxStage
								settings={filtersProcess.types}
								type='types'
							/>
						</AccordionDetails>
					</Accordion>
					<Accordion disableGutters className={styles.accordion}>
						<AccordionSummary expandIcon={<ExpandMoreIcon />}>
							<FormControlLabel
								label={
									<Typography className={styles.typography}>Статус</Typography>
								}
								onClick={event => event.stopPropagation()}
								onFocus={event => event.stopPropagation()}
								control={
									<Checkbox
										checked={filtersProcess.statuses.every(item =>
											selectedFilters.statuses.includes(item)
										)}
										onChange={() =>
											dispatch(
												toggleAllFilters({
													settings: filtersProcess.statuses,
													type: 'statuses',
												})
											)
										}
										name='Статус'
									/>
								}
							/>
						</AccordionSummary>
						<AccordionDetails>
							<FiltersCheckboxStage
								settings={filtersProcess.statuses}
								type='statuses'
							/>
						</AccordionDetails>
					</Accordion>
				</>
			)}
			<Divider />
			<FormControlLabel
				key={'showCompletedProcess1'}
				label={
					<Typography className={styles.typography}>
						Показывать завершённые
					</Typography>
				}
				className={styles.showCompleted}
				control={
					<Checkbox
						checked={showCompleted}
						onChange={() => dispatch(toggleShowCompleted())}
						name={'Показывать завершённые'}
					/>
				}
			/>
		</List>
	)
}

export default FiltersListProcess
