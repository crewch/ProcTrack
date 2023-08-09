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
import { useQuery } from '@tanstack/react-query'
import { filtersService } from '../../../../services/filters.ts'
import { useAppDispatch, useAppSelector } from '../../../../hooks/reduxHooks.ts'
import { toggleAllFilters } from '../../../../store/filterStageSlice/filterStageSlice.ts'
import FiltersCheckboxProcess from './FiltersCheckbox/FiltersCheckboxStage.tsx'
import styles from './FiltersList.module.scss'

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

	return (
		<List component='nav' className={styles.list}>
			{isLoading && <LinearProgress />}
			{isSuccess && filtersStage && (
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
			)}
		</List>
	)
}

export default FiltersListStage
