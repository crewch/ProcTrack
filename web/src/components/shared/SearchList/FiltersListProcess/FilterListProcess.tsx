import {
	Accordion,
	AccordionDetails,
	AccordionSummary,
	Checkbox,
	FormControlLabel,
	LinearProgress,
	List,
	Typography,
} from '@mui/material'
import ExpandMoreIcon from '@mui/icons-material/ExpandMore'
import styles from './FilterListProcess.module.scss'
import { useQuery } from '@tanstack/react-query'
import { filtersService } from '../../../../services/filters'
import { useAppDispatch, useAppSelector } from '../../../../hooks/reduxHooks'
import { toggleAllFilters } from '../../../../store/filterProcessSlice/filterProcessSlice'
import FiltersCheckbox from './FiltersCheckbox/FiltersCheckbox'

const FilterListProcess = () => {
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
							<FiltersCheckbox
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
							<FiltersCheckbox settings={filtersProcess.types} type='types' />
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
							<FiltersCheckbox
								settings={filtersProcess.statuses}
								type='statuses'
							/>
						</AccordionDetails>
					</Accordion>
				</>
			)}
		</List>
	)
}

export default FilterListProcess
