import { FormControlLabel, FormGroup, Typography } from '@mui/material'
import Checkbox from '@mui/material/Checkbox'
import { FC, memo } from 'react'
import { useAppSelector, useAppDispatch } from '@/hooks/reduxHooks'
import { toggleFilter } from '@/store/filterProcessSlice/filterProcessSlice'
import styles from './FiltersCheckbox.module.scss'

interface FiltersCheckboxStageProps {
	settings: string[]
	type: 'priorities' | 'statuses' | 'types'
}

const FiltersCheckboxStage: FC<FiltersCheckboxStageProps> = memo(
	({ settings, type }) => {
		const selectedFilters = useAppSelector(state => state.filterStages)
		const dispatch = useAppDispatch()

		return (
			<FormGroup className={styles.formGroup}>
				{settings.map((setting, index) => (
					<FormControlLabel
						key={index}
						label={
							<Typography className={styles.typography}>{setting}</Typography>
						}
						control={
							<Checkbox
								checked={selectedFilters[type].includes(setting)}
								onChange={() => dispatch(toggleFilter({ setting, type }))}
								name={setting}
							/>
						}
					/>
				))}
			</FormGroup>
		)
	}
)

export default FiltersCheckboxStage
