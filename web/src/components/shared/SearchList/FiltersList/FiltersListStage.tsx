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
import { filtersService } from '../../../../services/filters.ts'
import { useAppDispatch, useAppSelector } from '../../../../hooks/reduxHooks.ts'
import {
	toggleAllFilters,
	toggleShowApproved,
} from '../../../../store/filterStageSlice/filterStageSlice.ts'
import FiltersCheckboxProcess from './FiltersCheckbox/FiltersCheckboxStage.tsx'
import styles from './FiltersList.module.scss'
import FiltersCheckboxStage from './FiltersCheckbox/FiltersCheckboxStage.tsx'

const FiltersListStage = () => {
	const {
		data: filtersStage,
		isLoading,
		isSuccess,
	} = useQuery({
		queryKey: ['filtersStage'],
		queryFn: filtersService.getFiltersStage,
	})

	const dispatch = useAppDispatch()
	const selectedFilters = useAppSelector(state => state.filterStages)
	const showCompleted = useAppSelector(state => state.filterStages.showApproved)

	return (
		<List component='nav' className={styles.list}>
			{isLoading && <LinearProgress />}
			{isSuccess && filtersStage && (
				<>
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
										checked={filtersStage.statuses.every(item =>
											selectedFilters.statuses.includes(item)
										)}
										onChange={() =>
											dispatch(
												toggleAllFilters({
													settings: filtersStage.statuses,
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
							<FiltersCheckboxProcess
								settings={filtersStage.statuses}
								type='statuses'
							/>
						</AccordionDetails>
					</Accordion>
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
										checked={filtersStage.priorities.every(item =>
											selectedFilters.priorities.includes(item)
										)}
										onChange={() =>
											dispatch(
												toggleAllFilters({
													settings: filtersStage.priorities,
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
								settings={filtersStage.priorities}
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
										checked={filtersStage.types.every(item =>
											selectedFilters.types.includes(item)
										)}
										onChange={() =>
											dispatch(
												toggleAllFilters({
													settings: filtersStage.types,
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
								settings={filtersStage.types}
								type='types'
							/>
						</AccordionDetails>
					</Accordion>
				</>
			)}
			<Divider />
			<FormControlLabel
				key={'showCompletedProcess2'}
				label={
					<Typography className={styles.typography}>
						Показывать согласованные
					</Typography>
				}
				className={styles.showCompleted}
				control={
					<Checkbox
						checked={showCompleted}
						onChange={() => dispatch(toggleShowApproved())}
						name={'Показывать завершённые'}
					/>
				}
			/>
		</List>
	)
}

export default FiltersListStage
